using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Shared.Pagination
{
    public class PaginationObject<T>
    {
        public PaginationObject(List<T> dataList, int Count, int pageIndex = 0, int pageCount = 0)
        {
            PageCount = pageCount;
            TotalCount = Count;
            PageIndex = pageIndex;
            DataList = dataList;
        }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
        public List<T> DataList { get; set; }
    }
}
