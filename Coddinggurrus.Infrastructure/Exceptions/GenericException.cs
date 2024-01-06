using Coddinggurrus.Infrastructure.APIModels;
using Coddinggurrus.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Infrastructure.Exceptions
{
    [Serializable]
    public class GenericException : Exception
    {
        public GenericException()
        : base() { }

        public GenericException(ErrorMessages message)
            : base(message.ToString()) { }

        public GenericException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public GenericException(string message, Exception innerException)
            : base(message, innerException) { }

        public GenericException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected GenericException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    public class ExceptionHandling
    {
        public static void ExceptionResponse(Exception exception, ref BasicResponse basicResponse)
        {
            StringBuilder exceptionMessage = new StringBuilder();
            if (exception.InnerException != null)
            {
                if (exception.Message != null)
                {
                    exceptionMessage.Append("Message: " + exception.Message + ",\n Inner Exception: " + exception.InnerException.Message);
                }
                else
                {
                    exceptionMessage.Append("Inner Exception: " + exception.InnerException.Message);
                }
            }
            else if (exception.Message != null)
            {
                exceptionMessage.Append("Message: " + exception.Message);
            }
            else
            {
                exceptionMessage.Append("There is something going wrong, Please contact with administrator");
            }
            basicResponse.Data = new List<string>();
            basicResponse.Success = false;
            basicResponse.ErrorMessage = Convert.ToString(exceptionMessage);
        }
    }
}
