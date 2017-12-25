using System;
using System.Runtime.Serialization;

namespace Pixstock.Nc.Srv.Common.Exception
{
    public class InterfaceOperationException : ApplicationException
    {
        public InterfaceOperationException()
        {
        }

        public InterfaceOperationException(string message) : base(message)
        {
        }

        public InterfaceOperationException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected InterfaceOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}