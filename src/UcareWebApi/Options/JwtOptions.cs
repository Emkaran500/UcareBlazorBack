using System.Text;

namespace UcareApp.Options
{
    public class JwtOptions
    {
        public required string Key { get; set; }
        public byte[] KeyInBytes => Encoding.ASCII.GetBytes(Key);
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
    }
}