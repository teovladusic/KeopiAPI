using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Models
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public static PagedList<T> ToPagedList(List<T> items, int pageNumber, int pageSize)
        {
            var pagedList = items.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            return new PagedList<T>(pagedList, items.Count, pageNumber, pageSize);
        }

        public PagedList(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            AddRange(items);
        }
    }
}
