using eShopsolution.Viewmodels.Comons;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopsolution.Viewmodels.System
{
    public class GetUserPagingRequest : PagingRequestBase
    {

        public String Keyword { get; set; }
    }
}
