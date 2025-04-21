using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Shared.Pagination
{
    public class PaginationHelper
    {
        public static async Task<PaginationObject<T>> CreateAsync<T>(IQueryable<T> query, int pageIndex = 1, int pageSize = 100)
        {
            if (pageIndex <= 0) pageIndex = 1;

            int totalCount = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            int skip = (pageIndex - 1) * pageSize;
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            return new PaginationObject<T>(items, totalCount, pageIndex, totalPages);
        }
    }
}
