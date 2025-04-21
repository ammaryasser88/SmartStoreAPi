using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.Responses
{
    public class ServiceResult
    {
        public bool result { get; }
        public string message { get; }

        public ServiceResult(bool result, string message)
        {
            this.result = result;
            this.message = message;
        }

        public static ServiceResult Success(string message)
            => new ServiceResult(true, message);
        public static ServiceResult Failure(string message)
            => new ServiceResult(false, message);
    }
}
