namespace JmfJwtHelper
{
    [Serializable]
    public class JwtHelperException : Exception
    {
        public JwtHelperException() : base()
        {
        }

        public JwtHelperException(string message) : base(message)
        {
        }

        public JwtHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected JwtHelperException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}