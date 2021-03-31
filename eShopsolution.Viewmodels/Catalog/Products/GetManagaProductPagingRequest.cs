using eShopsolution.Viewmodels.Comons;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopsolution.Viewmodels.Catalog.Products
{
    public class GetManagaProductPagingRequest : PagingRequestBase
    {
        public String Keyword { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
