using AutoMapper;
using CoreService.Contracts.Courses;
using CoreService.Contracts.Groups;
using CoreService.Contracts.Lessons;
using CoreService.Contracts.Students;
using CoreService.Contracts.Teachers;
using Domain.Entities;

namespace Application.Mapping
{
    /// <summary>
    /// Запросы из контрактов → сущности. Маппинг живёт здесь, а не в DTO:
    /// контракты не зависят ни от AutoMapper, ни от Domain.
    /// </summary>
    public sealed class RequestMappingProfile : Profile
    {
        public RequestMappingProfile()
        {
            CreateMap<CreateCourseRequest, Course>();
            CreateMap<UpdateCourseRequest, Course>()
                .ForMember(course => course.ID, opt => opt.MapFrom(request => request.Id));

            CreateMap<CreateLessonRequest, Lesson>()
                .ForMember(lesson => lesson.GroupID, opt => opt.MapFrom(request => request.GroupId));
            CreateMap<UpdateLessonRequest, Lesson>()
                .ForMember(lesson => lesson.ID, opt => opt.MapFrom(request => request.Id))
                .ForMember(lesson => lesson.GroupID, opt => opt.MapFrom(request => request.GroupId));

            // Login/Password/RoleId уходят в AuthService событием, в сущность не попадают
            CreateMap<CreateStudentRequest, Student>();
            CreateMap<UpdateStudentRequest, Student>()
                .ForMember(student => student.StudentId, opt => opt.MapFrom(request => request.Id));

            CreateMap<CreateTeacherRequest, Teacher>();
            CreateMap<UpdateTeacherRequest, Teacher>()
                .ForMember(teacher => teacher.TeacherId, opt => opt.MapFrom(request => request.Id));

            CreateMap<CreateGroupRequest, Group>()
                .ForMember(group => group.CourseID, opt => opt.MapFrom(request => request.CourseId))
                .ForMember(group => group.TeacherGroups, opt => opt.MapFrom(request =>
                    request.TeacherIds.Select(id => new TeacherGroup { TeacherID = id }).ToList()))
                .ForMember(group => group.StudentGroups, opt => opt.MapFrom(request =>
                    request.StudentIds.Select(id => new StudentGroup { StudentID = id }).ToList()));
            CreateMap<UpdateGroupRequest, Group>()
                .ForMember(group => group.ID, opt => opt.Ignore())
                .ForMember(group => group.CourseID, opt => opt.MapFrom(request => request.CourseId))
                .ForMember(group => group.TeacherGroups, opt => opt.MapFrom(request =>
                    request.TeacherIds.Select(id => new TeacherGroup { TeacherID = id }).ToList()))
                .ForMember(group => group.StudentGroups, opt => opt.MapFrom(request =>
                    request.StudentIds.Select(id => new StudentGroup { StudentID = id }).ToList()));
        }
    }
}
