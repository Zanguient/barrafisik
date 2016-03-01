using System;
using System.Collections.Generic;

namespace BarraFisik.API.Models
{
    public class PagedResult<T> where T: class
    {
        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public int PageCount { get; private set; }

        public long TotalRecordCount { get; set; }

        public PagedResult(IEnumerable<T> items, int pageNo, int pageSize, long totalRecordCount)
        {
            Items = new List<T>(items);

            PageNo = pageNo;
            PageSize = pageSize;
            TotalRecordCount = totalRecordCount;

            PageCount = totalRecordCount > 0
                        ? (int)Math.Ceiling(totalRecordCount / (double)PageSize)
                        : 0;
        }

        public List<T> Items { get; set; }
    }
}