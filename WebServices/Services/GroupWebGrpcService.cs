using DataMath.Dto;
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
                TeacherId = newgroup.TeacherId ?? 0,
            });
            return new CreateGroupDto()
            {
                Name = res.Name,
                TeacherId = res.TeacherId,
            };
        }

        public async Task<GetGroupDto> GetGroupAsync(int Id)
        {
            var res = await _client.GetGroupAsync(new GroupRequest { Id = Id });
            return new GetGroupDto
            {
                Id = res.Id,
                Name = res.Name,
                TeacherId = res.TeacherId
            };
        }
    }
}
