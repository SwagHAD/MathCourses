using Application.Commands.Create;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers.Create
{
    public sealed class CreateUserHandler(IAuthDbContext DbContext) : IRequestHandler<CreateUserCommand, int>
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
            await DbContext.Users.AddAsync(user, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}
