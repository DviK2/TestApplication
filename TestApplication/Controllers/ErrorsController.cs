using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Exceptions;
using TestApplication.Services;

namespace TestApplication.Controllers
{
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public MyErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            var code = 500;

            if (exception is MaxCountException || exception is MinNumberException || exception is MaxNumberException) code = 400; // Bad Request

            Response.StatusCode = code;

            return new MyErrorResponse(exception);
        }
    }

    public class MyErrorResponse
    {
        public string Type { get; set; }
        public string Message { get; set; }
        //public string StackTrace { get; set; }

        public MyErrorResponse(Exception ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            //StackTrace = ex.ToString();
        }
    }
}
