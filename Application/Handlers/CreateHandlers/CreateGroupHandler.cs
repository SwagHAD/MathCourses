using Application.DTO.GroupDTO;
using Application.Handlers.Base;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;

namespace Application.Handlers.CreateHandlers
{
    public sealed class CreateGroupHandler(IMapper Mapper, IMathDbContext DbContext) : IHandler<Group, CreateGroupDto>
    {
        public async Task<Group> Handle(CreateGroupDto dto)
        {
            var newgroup = Mapper.Map<Group>(dto);
            await DbContext.AddAsync(newgroup);
            await DbContext.SaveChangesAsync();
            return newgroup;
        }
    }
}
