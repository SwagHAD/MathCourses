namespace Swag.Web.Auth.Interfaces
{
    /// <summary>Где лежит пара токенов между перезагрузками страницы.</summary>
    public interface ITokenStorage
    {
        ValueTask<AuthTokens?> GetAsync();
        ValueTask SetAsync(AuthTokens tokens);
        ValueTask ClearAsync();
    }
}
