namespace Todoer.SharedKernel.Model
{
    public class Todo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public string User { get; set; }
    }
}
