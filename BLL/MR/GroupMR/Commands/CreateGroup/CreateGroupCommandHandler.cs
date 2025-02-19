using BLL.Common.Exceptions;
using BLL.Repository;
using DataMath;
using DataMath.Entities;
using MediatR;


namespace BLL.MR.GroupMR.Commands.CreateGroup
{
    public class CreateGroupCommandHandler(IGenericRepository<Group> groupRepository, IMathContext _context) : IRequestHandler<CreateGroupCommand, Group>
    {
        public async Task<Group> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.FindAsync(request.TeacherId);
            if (teacher == null)
            {
                throw new NotFoundException(nameof(Teacher), request.TeacherId);
            }
            var group = new Group
            {
                Name = request.Name,
                TeacherId = request.TeacherId,
                Students = request.Students
            };
            await groupRepository.AddAsync(group, cancellationToken);
            return group;
        }
    }
}
