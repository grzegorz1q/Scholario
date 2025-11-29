using AutoMapper;
using Scholario.Application.Dtos.ClassRoom;
using Scholario.Application.Interfaces;
using Scholario.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly IMapper _mapper;
        private readonly IClassroomRepository _classRoomRepository;
        public ClassroomService(IClassroomRepository classRoomRepository,IMapper mapper) 
        {
            _classRoomRepository = classRoomRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadClassRoomDto>> GetClassrooms()
        {
            var classrooms = await _classRoomRepository.GetClassrooms();
            return _mapper.Map<IEnumerable<ReadClassRoomDto>>(classrooms);
        }


    }
}
