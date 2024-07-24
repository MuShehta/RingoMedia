using System.ComponentModel.DataAnnotations;

namespace Department.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Logo { get; set; }

        public int? ParentDepartmentId { get; set; }
        public Department? ParentDepartment { get; set; }

        public ICollection<Department>? SubDepartments { get; set; }
        // Method to get the list of parent departments up to the top-level
        public List<Department> GetParentDepartments()
        {
            var parents = new List<Department>();
            var current = this;

            while (current.ParentDepartment != null)
            {
                parents.Add(current.ParentDepartment);
                current = current.ParentDepartment;
            }

            return parents;
        }
    }
}
