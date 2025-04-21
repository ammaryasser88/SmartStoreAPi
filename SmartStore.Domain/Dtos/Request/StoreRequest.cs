using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Dtos.Request
{
    public class StoreRequest
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
    }
}
