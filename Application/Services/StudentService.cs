using Application.DTO.StudentDto;
using Application.Responses;
using Application.Services.Base;
using Domain.Entities;

namespace Application.Services
{
    public class StudentService(IServiceProvider services) : ApplicationServiceBase<Student>(services)
    {
        public async Task<Response<StudentDto>> Create(CreateStudentDto dto)
        {
            try
            {
                var createvalidator = ValidatorFactory.GetValidator<CreateStudentDto>();
                var result = await createvalidator.ValidateAsync(dto);
                if (!result.IsValid)
                    return Response<StudentDto>.Fail(result.Errors.Select(e => e.ErrorMessage).ToList());
                var student = await CoreService.CreateItemAsync(new Student { Name = dto.Name });
                return Response<StudentDto>.Ok(new StudentDto { ID = student.ID, Name = student.Name }, "Студент успешно создан");
            }
            catch (Exception ex)
            {
                return Response<StudentDto>.Error(ex.Message);
            }
        }
        public async Task<Response<StudentDto>> Update(UpdateStudentDto dto)
        {
            try
            {
                var updatevalidator = ValidatorFactory.GetValidator<UpdateStudentDto>();
                var result = await updatevalidator.ValidateAsync(dto);
                if (!result.IsValid)
                    return Response<StudentDto>.Fail(result.Errors.Select(e => e.ErrorMessage).ToList());
                var student = await CoreService.UpdateItemAsync(new Student { ID = dto.ID, Name = dto.Name });
                return Response<StudentDto>.Ok(new StudentDto { ID = student.ID, Name = student.Name }, "Студент успешно обновлен");
            }
            catch (Exception ex)
            {
                return Response<StudentDto>.Error(ex.Message);
            }
        }
        public async Task<Response<StudentDto>> Delete(DeleteStudentDto dto)
        {
            try
            {
                var updatevalidator = ValidatorFactory.GetValidator<DeleteStudentDto>();
                var result = await updatevalidator.ValidateAsync(dto);
                if (!result.IsValid)
                    return Response<StudentDto>.Fail(result.Errors.Select(e => e.ErrorMessage).ToList());
                await CoreService.DeleteItemAsync(dto.ID);
                return Response<StudentDto>.Ok(null, "Студент успешно удален");
            }
            catch (Exception ex)
            {
                return Response<StudentDto>.Error(ex.Message);
            }
        }
    }
}
