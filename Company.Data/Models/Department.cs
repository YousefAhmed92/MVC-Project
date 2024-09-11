namespace Company.Data.Models
{
    public class Department :BaseEntity
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public ICollection<Employees>? employees { get; set; }
    }
}
