using Grpc.Core;
using MediatR;
using BLL.MR.GroupMR.Commands.CreateGroup;
using BLL.MR.GroupMR.Commands.DeleteGroup;
using BLL.MR.GroupMR.Commands.UpdateGroup;
using BLL.MR.GroupMR.Queries;
using GrpcService;
using DataMath.Entities;

public class GroupServiceGrpc(IMediator mediator) : GroupService.GroupServiceBase
{
    private readonly IMediator _mediator = mediator;

    public override async Task<GroupResponse> GetGroup(GroupRequest request, ServerCallContext context)
    {
        var groupDto = await _mediator.Send(new GetGroupDetailsQuery { Id = request.Id });

        return new GroupResponse
        {
            Id = groupDto.Id,
            Name = groupDto.Name,
            TeacherId = groupDto.TeacherId ?? 0
        };
    }

    public override async Task<GroupResponse> CreateGroup(CreateGroupRequest request, ServerCallContext context)
    {
        var group = await _mediator.Send(new CreateGroupCommand
        {
            Name = request.Name,
            TeacherId = request.TeacherId,
            Students = request.Students.Select(id => new Student { Id = id }).ToList()
        });

        return new GroupResponse
        {
            Id = group.Id,
            Name = group.Name,
            TeacherId = group.TeacherId ?? 0
        };
    }

    public override async Task<DeleteGroupResponse> DeleteGroup(DeleteGroupRequest request, ServerCallContext context)
    {
        await _mediator.Send(new DeleteGroupCommand { Id = request.Id });

        return new DeleteGroupResponse { Success = true };
    }

    public override async Task<GroupResponse> UpdateGroup(UpdateGroupRequest request, ServerCallContext context)
    {
        await _mediator.Send(new UpdateGroupCommand
        {
            Id = request.Id,
            Name = request.Name,
            TeacherId = request.TeacherId,
            Students = request.Students.Select(id => new Student { Id = id }).ToList()
        });

        return new GroupResponse
        {
            Id = request.Id,
            Name = request.Name,
            TeacherId = request.TeacherId
        };
    }
}
