namespace SharedKernel.Contracts.Access
{
    /// <summary>
    /// На какие записи действует право роли. Значения упорядочены по ширине:
    /// из нескольких областей побеждает наибольшая.
    /// </summary>
    public enum AccessScope
    {
        /// <summary>Только свои записи: свой профиль, свои сдачи ДЗ.</summary>
        Own = 0,
        /// <summary>Записи, связанные с пользователем отношениями: уроки и группы, где он учится или преподаёт.</summary>
        Related = 1,
        /// <summary>Все записи.</summary>
        All = 2,
    }
}
