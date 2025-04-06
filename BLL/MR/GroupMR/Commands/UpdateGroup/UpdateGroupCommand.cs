﻿using DataMath.Entities;
using MediatR;

namespace BLL.MR.GroupMR.Commands.UpdateGroup
{
    public class UpdateGroupCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<Student> Students = new List<Student>();
    }
}
