namespace PokeSaveRomManager.Parser.Core.Models
{
    public class ParserResult<T>
    {
        public bool Success { get; set; }

        public T Data { get; set; }

        public List<string> Errors { get; set; } = [];

        public static ParserResult<T> SuccessResult(T data) => new()
        {
            Success = true,
            Data = data
        };

        public static ParserResult<T> FailureResult(IEnumerable<string> errorMessages) => new()
        {
            Success = false,
            Errors = [.. errorMessages]
        };
    }
}
