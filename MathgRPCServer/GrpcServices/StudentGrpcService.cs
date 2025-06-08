using AutoMapper;
using BLL.MR.StudentMR.Command.CreateStudent;
using BLL.MR.StudentMR.Command.DeleteStudent;
using BLL.MR.StudentMR.Command.UpdateStudent;
using BLL.MR.StudentMR.Queries;
using Core.MR.StudentMR.Queries;
using DataMath.Entities;
using DataMath.ServerVariables;
using MathgRPCServer.GrpcServices.Interfaces;
using MediatR;
using Shared.Math.Commons;

namespace MathgRPCServer.GrpcServices
{
    public class StudentGrpcService(IMediator mediator, IMapper mapper) : IGrpcService<Student>
    {
        private readonly IMediator mediator = mediator;
        private readonly IMapper mapper = mapper;
        public async Task<ServerResult<Student>> Create(RequestItem<Student> Request)
        {
            try
            {
                var entity = await mediator.Send(new CreateStudentCommand
                {
                    Name = Request.Item.Name,
                });
                return new ServerResult<Student>
                {
                    Data = entity,
                    HasError = false,
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<Student>
                {
                    HasError = true,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ServerResult<Student>> Delete(IdRequest IdPar)
        {
            try
            {
                await mediator.Send(new DeleteStudentCommand
                {
                    Id = IdPar.ID ?? -1
                });
                return new ServerResult<Student> 
                {
                    HasError = false,
                };
            }
            catch(Exception ex)
            {
                return new ServerResult<Student> 
                {
                    HasError = true,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ServerResult<ICollection<Student>>> GetAll(IdRequest IdPar)
        {
            try
            {
                var students = await mediator.Send(new GetAllStudentsDetailsQuery { });
                return new ServerResult<ICollection<Student>>
                {
                    Data = students,
                    HasError = false,
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<ICollection<Student>>
                {
                    HasError = true,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ServerResult<Student>> Get(IdRequest IdPar)
        {
            try
            {
                var student = await mediator.Send(new GetStudentDetailsQuery
                {
                    Id = IdPar.ID ?? -1
                });
                return new ServerResult<Student>
                {
                    HasError = false,
                    Data = mapper.Map<Student>(student)
                };
            }
            catch(Exception ex)
            {
                return new ServerResult<Student>
                {
                    HasError = true,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ServerResult<Student>> Update(RequestItem<Student> Request)
        {
            try
            {
                var student = await mediator.Send(new UpdateStudentCommand
                {
                    Id = Request.Item.Id,
                    Name = Request.Item.Name,
                });
                return new ServerResult<Student>
                {
                    HasError = false
                };
            }
            catch (Exception ex)
            {
                return new ServerResult<Student>
                {
                    HasError = true,
                    Message = ex.Message
                };
            }
        }
    }
}
