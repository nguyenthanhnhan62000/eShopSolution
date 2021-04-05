using System;
using System.Collections.Generic;
using System.Text;

namespace eShopsolution.Viewmodels.Comons
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public String[] ValidationError { get; set; }


        public ApiErrorResult()
        {

        }
        public ApiErrorResult(String message)
        {
            IsSuccessed = false;
            Message = message;

        }
        public ApiErrorResult(String[] validationError)
        {
            IsSuccessed = false;
            ValidationError = validationError;

        }
    }
}
