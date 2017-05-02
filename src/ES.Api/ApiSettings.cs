using System;

namespace ES.Api
{
    public class ApiSettings
    {
        public string Origins { get; set; }

        public string[] AllowedOrigins
            => Origins?.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries) ?? new string[] {};
    }
}
