namespace Domain.Classes.Values
{
    public sealed record DateRange
    {
        public DateOnly Start { get; }
        public DateOnly? End { get; }
        public DateRange(DateOnly start, DateOnly? end)
        {
            if (end != null && end < start)
            {
                throw new ArgumentException("End date cannot be before start date.", nameof(end));
            }

            Start = start;
            End = end;
        }

        public DateRange(DateTime start, DateTime? end)
        {
            if (end != null && end < start)
            {
                throw new ArgumentException("End date cannot be before start date.", nameof(end));
            }

            DateOnly startAsDateOnly = new DateOnly(start.Year, start.Month, start.Day);
            DateOnly? endAsDateOnly = end == null ? null : new DateOnly(end.Value.Year, end.Value.Month, end.Value.Day);

            Start = startAsDateOnly;
            End = endAsDateOnly;
        }
    }
}
