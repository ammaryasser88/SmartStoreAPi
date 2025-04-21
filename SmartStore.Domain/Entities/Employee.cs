using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public string JobTitle { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
