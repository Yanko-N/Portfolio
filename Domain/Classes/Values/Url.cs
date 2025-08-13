namespace Domain.Classes.Values
{
    public sealed record Url
    {
        public string Value { get; }
        public Url(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Invalid URL.", nameof(value));
            }

            try
            {
                var url = new Uri(value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid url address format.", nameof(value), ex);
            }

            Value = value.Trim();
        }
        public override string ToString() => Value;
    }
}
