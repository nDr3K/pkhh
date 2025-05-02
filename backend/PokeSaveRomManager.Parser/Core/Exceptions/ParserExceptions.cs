namespace PokeSaveRomManager.Parser.Core.Exceptions
{
    public class ParserException : Exception
    {
        public ParserException(string message) : base(message) { }
        public ParserException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class UnsupportedRomException : ParserException
    {
        public UnsupportedRomException(string message) : base(message) { }
    }

    public class UnsupportedSaveException : ParserException
    {
        public UnsupportedSaveException(string message) : base(message) { }
    }

    public class DataOutOfBoundsException : ParserException
    {
        public DataOutOfBoundsException(string context) : base($"Data access out of bounds: {context}") { }
    }
}