using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Auth.Seeds.Seeders
{
    public static class ObjectTypeSeeder
    {
        private static readonly ObjectType[] _objectTypes = new ObjectType[]
        {
            new ObjectType { Name = "User", ServiceType = ServiceType.AuthService, NormalizedName = "Пользователь" },
            new ObjectType { Name = "Role", ServiceType = ServiceType.AuthService, NormalizedName = "Роль" },
            new ObjectType { Name = "Permission", ServiceType = ServiceType.AuthService, NormalizedName = "Доступ" },
            new ObjectType { Name = "Student", ServiceType = ServiceType.CoreService, NormalizedName = "Студент" },
            new ObjectType { Name = "Group", ServiceType = ServiceType.CoreService, NormalizedName = "Группа" },
            new ObjectType { Name = "Teacher", ServiceType = ServiceType.CoreService, NormalizedName = "Преподаватель" },
            new ObjectType { Name = "Lesson", ServiceType = ServiceType.CoreService, NormalizedName = "Урок" },
            new ObjectType { Name = "Course", ServiceType = ServiceType.CoreService, NormalizedName = "Курс" },
            new ObjectType { Name = "Grade", ServiceType = ServiceType.CoreService, NormalizedName = "Оценка" },
            new ObjectType { Name = "Homework", ServiceType = ServiceType.CoreService, NormalizedName = "Домашнее задание" },
            new ObjectType { Name = "HomeworkSubmission", ServiceType = ServiceType.CoreService, NormalizedName = "Сдача домашнего задания" },
            new ObjectType { Name = "LessonMaterial", ServiceType = ServiceType.CoreService, NormalizedName = "Материал урока" },
            new ObjectType { Name = "StreamLesson", ServiceType = ServiceType.CoreService, NormalizedName = "Прямой эфир урока" },
        };
        public static async Task SeedAsync(IAuthDbContext dbContext, CancellationToken cancellationToken = default)
        {
            await SeedObjectTypesAsync(dbContext, cancellationToken);
        }
        private static async ValueTask SeedObjectTypesAsync(IAuthDbContext context, CancellationToken cancellationToken)
        {
            foreach(var type in _objectTypes) 
            {
                if(await context.ObjectTypes.AnyAsync(x => x.Name == type.Name && x.ServiceType == type.ServiceType))
                    continue;
                await context.ObjectTypes.AddAsync(type, cancellationToken);
            }
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
