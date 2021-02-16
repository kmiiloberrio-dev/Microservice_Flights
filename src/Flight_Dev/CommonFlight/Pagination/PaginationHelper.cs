using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CommonFlight.Pagination
{
    public static class PaginationHelper
    {
        public static async Task<DataCollection<T>> GetPagedAsync<T>(
             this IQueryable<T> query,
             int page,
             int take)
        {
            var originalPages = page;

            if (page > 0)
            {
                page--;
            }
            else
            {
                page = 0;
            }

            if (take == 0)
            {
                take = 10;
            }

            if (page > 0)
                page *= take;

            var result = new DataCollection<T>
            {
                Items = await query.Skip(page).Take(take).ToListAsync(),
                Total = await query.CountAsync(),
                Page = originalPages
            };

            if (result.Total > 0)
            {
                result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
            }

            return result;
        }
    }
}
