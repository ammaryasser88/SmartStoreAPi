using SmartStore.Domain.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Dtos.Response
{
    public class ItemUnitResponse:ItemUnitRequest
    {
       public int ItemUnitId { get; set; }
       
    }
}
