using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes.Values
{
    public sealed record Email
    {
        public string Value { get; }
        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Invalid email address.", nameof(value));
            }

            try
            {
                var email = new MailAddress(value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid email address format.", nameof(value), ex);
            }
           
            Value = value.Trim();
        }

        public override string ToString() => Value;
    }
}
