using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Dto.Collections
{

        public class PaginatedList<T>
        {
            public PaginatedList(long currentPage, long totalItems, long itemsPerPage)
            {
                CurrentPage = currentPage;
                TotalItems = totalItems;
                ItemsPerPage = itemsPerPage;
                TotalPages = (totalItems + itemsPerPage - 1) / itemsPerPage;
                Items = new List<T>();

                /*
                totalItems | itemsPerPage | totalPages | totalPages should be
                15           15             1            1
                14           15             1            1
                16           15             2            2
                30           15             2            2
                31           15             3            3
                 */
            }
            public List<T> Items { get; set; }
            public long CurrentPage { get; private set; }
            public long TotalItems { get; private set; }
            public long ItemsPerPage { get; private set; }
            public long TotalPages { get; private set; }
        }
    }

