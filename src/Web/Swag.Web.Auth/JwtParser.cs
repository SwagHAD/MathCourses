using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Swag.Web.Auth
{
    /// <summary>
    /// Читает payload JWT без проверки подписи: подпись проверяют сервисы, клиенту нужны только claims и срок.
    /// JwtSecurityTokenHandler на сервере сокращает типы claims (nameid, role) — возвращаем полные.
    /// </summary>
    public static class JwtParser
    {
        private static readonly Dictionary<string, string> ShortClaimTypes = new()
        {
            ["nameid"] = ClaimTypes.NameIdentifier,
            ["role"] = ClaimTypes.Role,
            ["unique_name"] = ClaimTypes.Name,
        };

        public static List<Claim> ReadClaims(string token)
        {
            using var payload = ReadPayload(token);
            var claims = new List<Claim>();
            foreach (var property in payload.RootElement.EnumerateObject())
            {
                var type = ShortClaimTypes.GetValueOrDefault(property.Name, property.Name);
                if (property.Value.ValueKind == JsonValueKind.Array)
                    claims.AddRange(property.Value.EnumerateArray().Select(item => new Claim(type, item.ToString())));
                else
                    claims.Add(new Claim(type, property.Value.ToString()));
            }
            return claims;
        }

        public static DateTimeOffset GetExpiry(string token)
        {
            using var payload = ReadPayload(token);
            return payload.RootElement.TryGetProperty("exp", out var exp)
                ? DateTimeOffset.FromUnixTimeSeconds(exp.GetInt64())
                : DateTimeOffset.MinValue;
        }

        private static JsonDocument ReadPayload(string token)
        {
            var parts = token.Split('.');
            if (parts.Length != 3)
                throw new FormatException("Некорректный JWT");
            var base64 = parts[1].Replace('-', '+').Replace('_', '/');
            base64 = base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');
            return JsonDocument.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(base64)));
        }
    }
}
