
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using System.Text.Json.Nodes;

using Scholario.Application.Interfaces;
using Scholario.Application.Services;
using Scholario.Application.Dtos;
using Scholario.Application.Dtos.Validators;
using Scholario.Application.Authentication;

using Scholario.Domain.Interfaces;
using Scholario.Domain.Entities;

using Scholario.Infrastructure.Persistence;
using Scholario.Infrastructure.Repositories;

using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// AUTHENTICATION - JWT
// ==========================================

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
})
.AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;

    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

// ==========================================
// CORS
// ==========================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ==========================================
// CONTROLLERS + JSON
// ==========================================

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());

        options.JsonSerializerOptions.Converters.Add(
            new TimeSpanConverter());
    });

// ==========================================
// FLUENT VALIDATION
// ==========================================

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

// ==========================================
// AUTOMAPPER
// ==========================================

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});

// ==========================================
// SWAGGER / OPENAPI
// ==========================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Scholario API",
        Version = "v1"
    });

    // JWT Bearer authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter JWT token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    // Global security requirement
    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });

    // TimeSpan schema
    options.MapType<TimeSpan>(() => new OpenApiSchema
    {
        Type = JsonSchemaType.String,
        Example = JsonValue.Create("00:00")
    });
});

// ==========================================
// DATABASE - SQL SERVER
// ==========================================

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ==========================================
// REPOSITORIES
// ==========================================

builder.Services.AddScoped<IDescriptiveAssessmentRepository, DescriptiveAssessmentRepository>();
builder.Services.AddScoped<IGradeRepository, GradeRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<ILessonHourRepository, LessonHourRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IParentRepository, ParentRepository>();
builder.Services.AddScoped<IScheduleEntryRepository, ScheduleEntryRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IStudentAttendanceRepository, StudentAttendanceRepository>();
builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();

// ==========================================
// SERVICES
// ==========================================

builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IScheduleEntryService, ScheduleEntryService>();
builder.Services.AddScoped<ILessonHourService, LessonHourService>();
builder.Services.AddScoped<IStudentAttendanceService, StudentAttendanceService>();
builder.Services.AddScoped<IClassroomService, ClassroomService>();

// ==========================================
// PASSWORD HASHING + VALIDATION
// ==========================================

builder.Services.AddScoped<IPasswordHasher<Person>, PasswordHasher<Person>>();

builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

// ==========================================
// BUILD APPLICATION
// ==========================================

var app = builder.Build();

// ==========================================
// SWAGGER UI
// ==========================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ==========================================
// DATABASE INITIALIZATION
// ==========================================

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    dbContext.Database.Migrate();

    var passwordHasher = scope.ServiceProvider
        .GetRequiredService<IPasswordHasher<Person>>();

    var prepDatabase = new PrepDatabase(dbContext, passwordHasher);
    prepDatabase.Seed();
}

// ==========================================
// HTTP PIPELINE
// ==========================================

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
