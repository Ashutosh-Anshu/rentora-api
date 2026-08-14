namespace Rentora.Application.Common.Shared.Responses
{
    public class Result<T>
    {
        public bool Success { get; init; }

        public string Message { get; init; } = string.Empty;

        public T? Data { get; init; }

        public IEnumerable<Error> Errors { get; init; } = [];

        // Success
        public static Result<T> Ok(
            T data,
            string message = "Success")
        {
            return new Result<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        // Single error
        public static Result<T> Fail(
            string errorMessage,
            string message = "Failed")
        {
            return new Result<T>
            {
                Success = false,
                Message = message,
                Errors =
                [
                    new Error(errorMessage)
                ]
            };
        }

        // Multiple errors
        public static Result<T> Fail(
            IEnumerable<Error> errors,
            string message = "Failed")
        {
            return new Result<T>
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }
    }
}
