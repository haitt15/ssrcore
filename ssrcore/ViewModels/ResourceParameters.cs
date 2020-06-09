using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.ViewModels
{
    public class ResourceParameters
    {
        private const int maxPageCount = 20;
        public int Page { get; set; } = 1;
        private int _pageCount = maxPageCount;
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = (value > maxPageCount) ? maxPageCount : value; }
        }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
        internal double GetTotalPages(int count)
        {
            return Math.Ceiling(count / (double)PageCount);
        }
    }
}
