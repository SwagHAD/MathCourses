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

    public override async Task<StudentResponse> GetStudent(StudentRequest request, ServerCallContext context)
    {
        var studentDto = await _mediator.Send(new GetStudentDetailsQuery { Id = request.Id });

        return new StudentResponse
        {
            Id = studentDto.Id,
            Name = studentDto.Name
        };
    }

    public override async Task<StudentResponse> CreateStudent(CreateStudentRequest request, ServerCallContext context)
    {
        var student = await _mediator.Send(new CreateStudentCommand
        {
            Name = request.Name
        });

        return new StudentResponse
        {
            Id = student.Id,
            Name = student.Name
        };
    }

    public override async Task<DeleteStudentResponse> DeleteStudent(DeleteStudentRequest request, ServerCallContext context)
    {
        await _mediator.Send(new DeleteStudentCommand { Id = request.Id });

        return new DeleteStudentResponse { Success = true };
    }

    public override async Task<StudentResponse> UpdateStudent(UpdateStudentRequest request, ServerCallContext context)
    {
        await _mediator.Send(new UpdateStudentCommand
        {
            Id = request.Id,
            Name = request.Name
        });

        var responce = new StudentResponse
        {
            Id = request.Id,
            Name = request.Name
        };
        return responce;
    }
}
