namespace WebApp.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public string ExistingImagepath { get; set; }


        public IFormFile photo { get; set; }

    }
}
