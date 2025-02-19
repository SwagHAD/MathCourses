using System.ComponentModel.DataAnnotations;

namespace DataMath.Entities
{
    public class Group
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public int? TeacherId { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
