using DataMath.Dto;
using DataMath.Entities;
using MathgRPCServer.Grpc;
using WebServices.GrpcClientFactory.ClientFactory;

namespace WebServices.Services
{
    public class GroupWebGrpcService : IGroupService
    {
        private readonly GroupService.GroupServiceClient _client;

        public  GroupWebGrpcService(IGrpcClientFactory factory)
        {
            _client = factory.CreateClient<GroupService.GroupServiceClient>();
        }
        public async Task<CreateGroupDto> CreateGroupAsync(GetGroupDto newgroup)
        {
            var res = await _client.CreateGroupAsync(new CreateGroupRequest
            {
                Name = newgroup.Name,
                Teacher = new TeacherGrpc()
                {
                    Id = newgroup.Teacher.Id,
                    Name = newgroup.Teacher.Name,
                }

            });
            return new CreateGroupDto()
            {
                Name = res.Group.Name,
                Teacher = new Teacher()
                {
                    Id = res.Teacher.Id,
                    Name = res.Teacher.Name,
                },
                Students = res.Students.Select(s => new Student()
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList()
            };
        }

        public async Task<GetGroupDto> GetGroupAsync(int Id)
        {
            var res = await _client.GetGroupAsync(new GroupRequest { Id = Id });
            return new GetGroupDto
            {
                Id = res.Group.Id,
                Name = res.Group.Name,
                Teacher = new Teacher()
                {
                    Id = res.Teacher.Id,
                    Name = res.Teacher.Name,
                },
                Students = res.Students.Select(s => new Student()
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList()
            };
        }
    }
}
