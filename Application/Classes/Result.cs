namespace Application.Classes
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; } = default(T);
        private List<string> _errors { get; set; } = new List<string>();
        public string ErrorMessage
        {
            get
            {
                return _errors != null && _errors.Count > 0 ? string.Join(Environment.NewLine, _errors) : string.Empty;
            }
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Value = value,
                _errors = new List<string>()
            };
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Value = default,
                _errors = new List<string> { errorMessage }
            };
        }

        public static Result<T> Failure(List<string> errors)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Value = default,
                _errors = errors ?? new List<string>()
            };
        }
    }
}
