using BLL.MR.GroupMR.Commands.CreateGroup;
using BLL.MR.GroupMR.Commands.DeleteGroup;
using BLL.MR.GroupMR.Commands.UpdateGroup;
using BLL.MR.GroupMR.Queries;
using Core.MR.GroupMR.Queries;
using DataMath.Entities;
using DataMath.ServerVariables;
using MathgRPCServer.GrpcServices.Interfaces;
using MediatR;
using Shared.Math.Commons;

namespace MathgRPCServer.GrpcServices
{
    public class GroupGrpcService(IMediator mediator) : IGrpcService<Group>
    {
        private readonly IMediator mediator = mediator;

        public async Task<ServerResult<Group>> Create(RequestItem<Group> Request)
        {
            try
            {
                var group = await mediator.Send(new CreateGroupCommand
                {
                    Name = Request.Item.Name,
                    Teacher = Request.Item.Teacher,
                    Students = Request.Item.Students,
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

        public async Task<ServerResult<Group>> Delete(IdRequest IdPar)
        {
            try
            {
                await mediator.Send(new DeleteGroupCommand
                {
                    Id = IdPar.ID ?? -1
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

        public async Task<ServerResult<Group>> Get(IdRequest IdPar)
        {
            try
            {
                var group = await mediator.Send(new GetGroupDetailsQuery
                {
                    Id = IdPar.ID ?? -1,
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

        public async Task<ServerResult<ICollection<Group>>> GetAll(IdRequest IdPar)
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

        public async Task<ServerResult<Group>> Update(RequestItem<Group> Request)
        {
            try
            {
                await mediator.Send(new UpdateGroupCommand
                {
                    Name = Request.Item.Name,
                    Students = Request.Item.Students,
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
