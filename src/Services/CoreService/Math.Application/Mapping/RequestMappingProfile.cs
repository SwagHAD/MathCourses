using AutoMapper;
using CoreService.Contracts.Courses;
using CoreService.Contracts.Lessons;
using CoreService.Contracts.Persons;
using Domain.Entities;

namespace Application.Mapping
{
    /// <summary>
    /// Запросы из контрактов → сущности. Маппинг живёт здесь, а не в DTO:
    /// контракты не зависят ни от AutoMapper, ни от Domain.
    /// Группы маппятся вручную в обработчиках: состав группы обновляется построчно, а не заменой коллекции.
    /// </summary>
    public sealed class RequestMappingProfile : Profile
    {
        public RequestMappingProfile()
        {
            CreateMap<CreateCourseRequest, Course>();
            CreateMap<UpdateCourseRequest, Course>();

            CreateMap<CreateLessonRequest, Lesson>();
            // CourseId не трогаем: урок не переезжает между курсами
            CreateMap<UpdateLessonRequest, Lesson>()
                .ForMember(lesson => lesson.CourseId, opt => opt.Ignore());

            // Account уходит в AuthService событием, в сущность не попадает
            CreateMap<CreatePersonRequest, Person>()
                .AfterMap((_, person) => person.RefreshFullName());
            CreateMap<UpdatePersonRequest, Person>()
                .ForMember(person => person.UserId, opt => opt.Ignore())
                .AfterMap((_, person) => person.RefreshFullName());
        }
    }
}
