using Grpc.Core;
using MediatR;
using BLL.MR.GroupMR.Commands.CreateGroup;
using BLL.MR.GroupMR.Commands.DeleteGroup;
using BLL.MR.GroupMR.Commands.UpdateGroup;
using BLL.MR.GroupMR.Queries;
using DataMath.Entities;
using MathgRPCServer.Grpc;
public class GroupServiceGrpc(IMediator mediator) : GroupService.GroupServiceBase
{
    private readonly IMediator _mediator = mediator;

    public override async Task<GroupFullResponse> GetGroup(GroupRequest request, ServerCallContext context)
    {
        var group = await _mediator.Send(new GetGroupDetailsQuery { Id = request.Id });

        var response = new GroupFullResponse()
        {
            Group = new GroupGrpc()
            {
                Id = group.Id,
                Name = group.Name,
            },
            Teacher = new TeacherGrpc()
            {
                Id = group.Teacher.Id,
                Name = group.Teacher.Name,
            },
            Students =
            {
                group.Students.Select(s => new StudentGrpc
                {
                    Id = s.Id,
                    Name = s.Name,
                })
            }
        };
        return response;
    }
    public override async Task<GroupFullResponse> CreateGroup(CreateGroupRequest request, ServerCallContext context)
    {
        var group = await _mediator.Send(new CreateGroupCommand
        {
            Name = request.Name,
            Teacher = new Teacher()
            {
                Id = request.Teacher.Id,
                Name = request.Teacher.Name,
            },
            Students = request.Students.Select(student => new Student 
            { 
                Id = student.Id, 
                Name = student.Name ,
            }).ToList()
        });
        var response = new GroupFullResponse()
        {
            Group = new GroupGrpc()
            {
                Id = group.Id,
                Name = group.Name,
            },
            Teacher = new TeacherGrpc()
            {
                Id = group.Teacher.Id,
                Name = group.Teacher.Name,
            },
            Students = 
            {
                group.Students.Select(s => new StudentGrpc
                {
                    Id = s.Id,
                    Name = s.Name,
                })
            }
        };
        return response;
    }


    public override async Task<DeleteGroupResponse> DeleteGroup(DeleteGroupRequest request, ServerCallContext context)
    {
        await _mediator.Send(new DeleteGroupCommand { Id = request.Id });

        return new DeleteGroupResponse { Success = true };
    }

    public override async Task<GroupFullResponse> UpdateGroup(UpdateGroupRequest request, ServerCallContext context)
    {
        await _mediator.Send(new UpdateGroupCommand
        {
            Id = request.Id,
            Name = request.Name,
            Teacher = new Teacher
            {
                Id = request.Teacher.Id,
                Name = request.Teacher.Name,
            },
            Students = request.Students.Select(s => new Student
            {
                Id = s.Id,
                Name = s.Name,
            }).ToList()
        });


        return new GroupFullResponse();
    }

}
