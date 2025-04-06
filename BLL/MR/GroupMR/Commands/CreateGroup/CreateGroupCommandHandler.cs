using BLL.Common.Exceptions;
using BLL.Repository;
using DataMath.Entities;
using MediatR;


namespace BLL.MR.GroupMR.Commands.CreateGroup
{
    public class CreateGroupCommandHandler(IGenericRepository<Group> groupRepository) : IRequestHandler<CreateGroupCommand, Group>
    {
        public async Task<Group> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var teacher = await groupRepository.GetByIdAsync(request.Teacher.Id, cancellationToken);
            if (teacher == null)
            {
                throw new NotFoundException(nameof(Teacher), request.Teacher.Id);
            }
            var group = new Group
            {
                Name = request.Name,
                TeacherId = request.Teacher.Id,
                Teacher = request.Teacher,
                Students = request.Students
            };
            await groupRepository.AddAsync(group, cancellationToken);
            return group;
        }
    }
}
