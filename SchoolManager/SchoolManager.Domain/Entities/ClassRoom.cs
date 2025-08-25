namespace SchoolManager.Domain.Entities;

public class ClassRoom : BaseEntity
{
    public string Name { get; set; }
    public User HeadTeacher { get; set; }
    public User Student { get; set; }
}