using eShopsolution.Viewmodels.Comons;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopsolution.Viewmodels.Catalog.Products
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {

        public int? CategoryId { get; set; }

    }
}
