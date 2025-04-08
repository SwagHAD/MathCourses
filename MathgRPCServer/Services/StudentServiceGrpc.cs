using Grpc.Core;
using MediatR;
using BLL.MR.StudentMR.Queries;
using BLL.MR.StudentMR.Command.CreateStudent;
using BLL.MR.StudentMR.Command.DeleteStudent;
using BLL.MR.StudentMR.Command.UpdateStudent;
using MathgRPCServer.Grpc;
public class StudentServiceGrpc(IMediator mediator) : StudentService.StudentServiceBase
{
    private readonly IMediator _mediator = mediator;

    public override async Task<StudentFullResponse> GetStudent(StudentRequest request, ServerCallContext context)
    {
        var student = await _mediator.Send(new GetStudentDetailsQuery { Id = request.Id });

        return new StudentFullResponse
        {
            Student = new StudentGrpc()
            {
                Id = student.Id,
                Name = student.Name,
            },
            Groups =
            {
                student.Groups.Select(g => new GroupGrpc()
                {
                    Id = g.Id,
                    Name = g.Name,
                })
            }
        };

    }

    public override async Task<StudentFullResponse> CreateStudent(CreateStudentRequest request, ServerCallContext context)
    {
        var student = await _mediator.Send(new CreateStudentCommand
        {
            Name = request.Name
        });

        return new StudentFullResponse
        {
            Student = new StudentGrpc()
            {
                Id = student.Id,
                Name = student.Name,
            }
        };
    }

    public override async Task<DeleteStudentResponse> DeleteStudent(DeleteStudentRequest request, ServerCallContext context)
    {
        await _mediator.Send(new DeleteStudentCommand { Id = request.Id });

        return new DeleteStudentResponse { Success = true };
    }

    public override async Task<StudentFullResponse> UpdateStudent(UpdateStudentRequest request, ServerCallContext context)
    {
        await _mediator.Send(new UpdateStudentCommand
        {
            Id = request.Id,
            Name = request.Name
        });


        return new StudentFullResponse();
    }
}
