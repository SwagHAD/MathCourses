using DataMath.Entities;

namespace DataMath.Dto
{
    public class CreateGroupDto
    {
        public string Name { get; set; }
        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
