using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Models
{
    public class PagingParams
    {
        private const int MaxPageSize = 50;

        private int _currentPage = 1;
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
            }
        }

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > MaxPageSize)
                {
                    _pageSize = MaxPageSize;
                }
                else if (value < 1)
                {
                    _pageSize = 1;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }
    }
}
