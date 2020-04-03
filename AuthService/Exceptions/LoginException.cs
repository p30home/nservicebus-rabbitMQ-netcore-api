namespace AuthService.Exceptions
{
    [System.Serializable]
    public class LoginException : System.Exception
    {
        public LoginException() { }
        public LoginException(string message) : base(message) { }
        public LoginException(string message, System.Exception inner) : base(message, inner) { }
        protected LoginException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}