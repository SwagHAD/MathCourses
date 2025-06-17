using DataMath.Entities;

namespace DataMath.Dto
{
    public class GetGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Teacher Teacher { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
