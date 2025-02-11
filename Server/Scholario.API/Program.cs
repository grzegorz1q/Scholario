using Microsoft.EntityFrameworkCore;
using Scholario.Application.Interfaces;
using Scholario.Application.Services;
using Scholario.Domain.Interfaces.Repositories;
using Scholario.Infrastructure.Persistence;
using Scholario.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SQL Server configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//

//In Memory database configuration
/*builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("In Memory database"));*/
//

//Adds repositories to the Dependency Injection Container
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
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();

//Adds services to the Dependency Injection Container
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Preparing database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var prepDatabase = new PrepDatabase(dbContext);
    prepDatabase.Seed();
}
//

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
