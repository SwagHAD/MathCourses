using BLL.MR.GroupMR.Commands.CreateGroup;
using BLL.MR.GroupMR.Commands.DeleteGroup;
using BLL.MR.GroupMR.Commands.UpdateGroup;
using BLL.MR.GroupMR.Queries;
using Core.MR.GroupMR.Queries;
using DataMath.Entities;
using DataMath.ServerVariables;
using MathgRPCServer.GrpcServices.Interfaces;
using MediatR;

namespace MathgRPCServer.GrpcServices
{
    public class GroupGrpcService(IMediator mediator) : IGrpcService<Group>
    {
        private readonly IMediator mediator = mediator;

        public async Task<ServerResult<Group>> Create(Group common)
        {
            try
            {
                var group = await mediator.Send(new CreateGroupCommand
                {
                    Name = common.Name,
                    Teacher = common.Teacher,
                    Students = common.Students,
                });
                return new ServerResult<Group>
                {
                    Data = group,
                    HasError = false,
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<Group>
                {
                    HasError = true,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ServerResult<Group>> Delete(int id)
        {
            try
            {
                await mediator.Send(new DeleteGroupCommand
                {
                    Id = id
                });
                return new ServerResult<Group>
                {
                    HasError = false
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<Group>
                {
                    HasError = true,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ServerResult<Group>> Get(int Id)
        {
            try
            {
                var group = await mediator.Send(new GetGroupDetailsQuery
                {
                    Id = Id
                });
                return new ServerResult<Group>
                {
                    Data = group,
                    HasError = false,
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<Group>
                {
                    HasError = true,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ServerResult<ICollection<Group>>> GetAll()
        {
            try
            {
                var group = await mediator.Send(new GetAllGroupsDetailsQuery { });
                return new ServerResult<ICollection<Group>>
                {
                    Data = group,
                    HasError = false,
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<ICollection<Group>>
                {
                    HasError = true,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ServerResult<Group>> Update(Group common)
        {
            try
            {
                await mediator.Send(new UpdateGroupCommand
                {
                    Name = common.Name,
                    Students = common.Students,
                });
                return new ServerResult<Group>
                {
                    HasError = false
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<Group>
                {
                    HasError = true,
                    Message = ex.Message,
                };
            }
        }
    }
}
