﻿using AutoMapper;
using BLL.Common.Exceptions;
using BLL.MR.StudentMR.Queries.Dto;
using BLL.Repository;
using DataMath.Entities;
using MediatR;

namespace BLL.MR.StudentMR.Queries
{
    public class GetStudentDetailsQueryHandler(IGenericRepository<Student> studentRepository, IMapper mapper) : IRequestHandler<GetStudentDetailsQuery, StudentDetailsDto>
    {
        public async Task<StudentDetailsDto> Handle(GetStudentDetailsQuery request, CancellationToken cancellationToken)
        {
            var student = await studentRepository.GetByIdAsync(request.Id, cancellationToken);
            if(student is null)
            {
                throw new NotFoundException(nameof(Student), request.Id);
            }
            return mapper.Map<StudentDetailsDto>(student);
        }
    }
}
