using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities
{
    public class TransactionType
    {
        public int TypeId { get; set; }
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public bool   IsExpense { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public ICollection<Revenue> Revenues { get; set; } = new List<Revenue>();
    }
}
