using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Dtos.Request
{
    public class RevenueRequest
    {
        public int TypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;
        public string Notes { get; set; }
        public SafeTransaction? SafeTransaction { get; set; }
    }
}
