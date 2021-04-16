using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Utilities.Constants
{
    public class SystemConstants
    {
        public const string MainConnectionString = "eShopSolutionDb";
        public const String CartSession = "CartSession";

        public class AppSettings
        {
            public const String DefaultLanguageId = "DefaultLanguageId";
            public const String Token = "Token";
            public const String BaseAddress = "BaseAddress";

        }
        public class ProductSettings
        {
            public const int NumberOffFeaturedProducts = 4;

            public const int NumberOffLastedProducts = 6;
        }
        public class ProductConstants
        {
            public const string NA = "N/A";
        }
    }
}
