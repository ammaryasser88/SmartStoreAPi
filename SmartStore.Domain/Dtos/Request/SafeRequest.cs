using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Dtos.Request
{
    public class SafeRequest
    {
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public decimal Balance { get; set; }
        public bool? IsActive { get; set; }
    }
}
