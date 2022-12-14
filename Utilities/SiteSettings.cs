using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class SiteSettings
    {
        public JwtSettings JwtSettings { get; set; }
    }

    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string EncryptionKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationMinutes { get; set; }
        public int ClockSkew { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int RefreshTokenValidityInDays { get; set; }
    }
}
