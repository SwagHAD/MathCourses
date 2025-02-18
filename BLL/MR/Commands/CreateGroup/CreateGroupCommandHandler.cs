using BLL.Repository;
using DataMath;
using DataMath.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace BLL.MediatoR.Commands.CreateGroup
{
    public class CreateGroupCommandHandler(IGenericRepository<Group> groupRepository, IMathContext _context) : IRequestHandler<CreateGroupCommand, Group>
    {
        public async Task<Group> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.FindAsync(request.TeacherId);
            if (teacher == null)
            {
                throw new InvalidOperationException($"Teacher with ID {request.TeacherId} not found.");
            }
            var group = new Group
            {
                Name = request.Name,
                TeacherId = request.TeacherId,
            };
            await groupRepository.AddAsync(group, cancellationToken);
            return group;
        }
    }
}
