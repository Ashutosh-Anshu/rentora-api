namespace Rentora.Application.Common.Shared.Responses
{
    public class Result<T>
    {
        public bool Success { get; init; }

        public string Message { get; init; } = string.Empty;

        public T? Data { get; init; }

        public IEnumerable<Error>? Errors { get; init; }

        public static Result<T> Ok(T data, string message = "Success")
        {
            return new()
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static Result<T> Fail(IEnumerable<Error> errors, string message = "Failed")
        {
            return new()
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }
    }
}
