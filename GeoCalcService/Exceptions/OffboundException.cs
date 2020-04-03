namespace GeoCalcService.Exceptions
{
    [System.Serializable]
    public class OffboundException : System.Exception
    {
        public OffboundException() { }
        public OffboundException(string message) : base(message) { }
        public OffboundException(string message, System.Exception inner) : base(message, inner) { }
        protected OffboundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}