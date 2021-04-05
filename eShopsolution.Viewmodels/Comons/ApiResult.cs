using System;
using System.Collections.Generic;
using System.Text;

namespace eShopsolution.Viewmodels.Comons
{
    public class ApiResult<T>
    {
    
        public bool IsSuccessed { get; set; }

        public String Message { get; set; }

        public T ResultObj { get; set; }


    }
}
