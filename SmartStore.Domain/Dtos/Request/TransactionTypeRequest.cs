using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Dtos.Request
{
    public class TransactionTypeRequest
    {
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public bool   IsExpense { get; set; }
        public bool? IsActive { get; set; }
    }
}
