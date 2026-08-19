using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ResultPattern
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;

        public static Result Success(string message)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message

            };
        }

        public static Result Failure(string message)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}

