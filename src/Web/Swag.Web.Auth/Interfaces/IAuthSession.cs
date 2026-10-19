namespace Swag.Web.Auth.Interfaces
{
    /// <summary>Сессия пользователя: вход, выход и действующий access-токен с обновлением по refresh.</summary>
    public interface IAuthSession
    {
        /// <returns>null при успехе, иначе текст ошибки для пользователя.</returns>
        Task<string?> LoginAsync(string login, string password, CancellationToken cancellationToken = default);

        Task LogoutAsync(CancellationToken cancellationToken = default);

        /// <summary>Действующий access-токен; если он вот-вот истечёт — сначала обновляет. null — не вошли.</summary>
        Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновляет токены, если текущий access-токен всё ещё <paramref name="staleAccessToken"/>;
        /// если его уже обновил другой запрос — возвращает новый.
        /// </summary>
        /// <returns>Новый access-токен или null, если сессия закончилась.</returns>
        Task<string?> RefreshAsync(string staleAccessToken, CancellationToken cancellationToken = default);
    }
}
