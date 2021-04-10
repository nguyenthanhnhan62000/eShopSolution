using System;
using System.Collections.Generic;
using System.Text;

namespace eShopsolution.Viewmodels.Comons
{
    public class PageResult<T> : PageResultBase
    {

        public List<T> Items { get; set; }
       
    }
}
