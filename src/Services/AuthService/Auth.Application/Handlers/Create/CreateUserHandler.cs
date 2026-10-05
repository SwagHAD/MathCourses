using Application.Commands.Create;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.Create
{
    public sealed class CreateUserHandler(ISwagDbContext DbContext) : IRequestHandler<CreateUserCommand, int>
    {
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Login = request.Login,
                RoleId = request.RoleId
            };
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, request.Password);
            await DbContext.Set<User>().AddAsync(user, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}
