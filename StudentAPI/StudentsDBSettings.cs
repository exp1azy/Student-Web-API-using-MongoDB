namespace StudentAPI
{
    public class StudentsDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;

        public string StudentsCollection { get; set; } = null!;
        public string LecturersCollection { get; set; } = null!;
        public string ExamCollection { get; set; } = null!;
        public string StudGroupCollection { get; set; } = null!;
    }
}
