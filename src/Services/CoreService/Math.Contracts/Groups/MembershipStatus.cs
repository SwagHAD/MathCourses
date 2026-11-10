namespace CoreService.Contracts.Groups
{
    /// <summary>Доступ к материалам группы даёт только Active.</summary>
    public enum MembershipStatus
    {
        Active = 0,
        Completed = 1,
        Expelled = 2
    }
}
