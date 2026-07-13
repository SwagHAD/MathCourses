using Application.Queries.Base;
using Application.Responses;
using Domain.Entities;
using Domain.Enums;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetLessonQuery : IQuery<DefaultLessonResponse>
    {
        public int Id { get; set; }

        public async Task<bool> CheckResoursePermissionAsync(int userid, UserType userType, ISwagDbContext db, CancellationToken cancellationToken)
        {
            switch(userType)
            {
                case UserType.Admin:
                    return true;
                case UserType.Teacher:
                    {
                        return await db.Set<Lesson>().AnyAsync(g => g.ID == Id && 
                            g.GroupRef.TeacherGroups.Any(f => f.TeacherRef.UserId == userid), cancellationToken);
                    }
                case UserType.Student:
                    {
                        return await db.Set<Lesson>().AnyAsync(g => g.ID == Id && 
                            g.GroupRef.StudentGroups.Any(f => f.StudentRef.UserId == userid), cancellationToken);
                    }
                default:
                    return false;
            }
        }
    }
}
