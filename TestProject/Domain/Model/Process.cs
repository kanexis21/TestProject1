

namespace TestProject.Domain.Model
{
    public class Process
    {
        public int ProcessID { get; set; }
        public string ProcessCode { get; set; }
        public string? ProcessName { get; set; }
        public string? CategoryName { get; set; }          
        public string? OwnerDepartmentName { get; set; }    
    }


}
