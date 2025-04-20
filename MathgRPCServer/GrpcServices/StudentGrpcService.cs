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

namespace MathgRPCServer.GrpcServices
{
    public class StudentGrpcService(IMediator mediator, IMapper mapper) : IGrpcService<Student>
    {
        private readonly IMediator mediator = mediator;
        private readonly IMapper mapper = mapper;
        public async Task<ServerResult<Student>> Create(Student student)
        {
            try
            {
                var entity = await mediator.Send(new CreateStudentCommand
                {
                    Name = student.Name,
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

        public async Task<ServerResult<Student>> Delete(int id)
        {
            try
            {
                await mediator.Send(new DeleteStudentCommand
                {
                    Id = id
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

        public async Task<ServerResult<ICollection<Student>>> GetAll()
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

        public async Task<ServerResult<Student>> Get(int Id)
        {
            try
            {
                var student = await mediator.Send(new GetStudentDetailsQuery
                {
                    Id = Id
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

        public async Task<ServerResult<Student>> Update(Student common)
        {
            try
            {
                var student = await mediator.Send(new UpdateStudentCommand
                {
                    Id = common.Id,
                    Name = common.Name,
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
