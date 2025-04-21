using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain
{
    public interface IHasRowVersion
    {
        byte[] RowVersion { get; set; }
    }
}
