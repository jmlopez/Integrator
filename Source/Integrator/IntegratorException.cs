using System;
using System.Runtime.Serialization;
using FubuCore;

namespace Integrator
{
    [Serializable]
    public class IntegratorException : Exception
    {
        private readonly int _errorCode;
        private readonly string _message;

        public IntegratorException(int errorCode, string message)
            : base(message)
        {
            _errorCode = errorCode;
            _message = message;
        }

        private IntegratorException(int errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            _errorCode = errorCode;
            _message = message;
        }

        protected IntegratorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _errorCode = info.GetInt32("errorCode");
            _message = info.GetString("message");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("errorCode", _errorCode);
            info.AddValue("message", _message);
        }

        public IntegratorException(int errorCode, Exception inner, string template, params string[] substitutions)
            : this(errorCode, template.ToFormat(substitutions), inner)
        {
        }

        public IntegratorException(int errorCode, string template, params string[] substitutions)
            : this(errorCode, template.ToFormat(substitutions))
        {
        }

        public override string Message { get { return "Integrator Error {0}:  \n{1}".ToFormat(_errorCode, _message); } }

        public int ErrorCode { get { return _errorCode; } }
    }
}