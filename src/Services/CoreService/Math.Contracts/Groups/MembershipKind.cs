namespace CoreService.Contracts.Groups
{
    /// <summary>Роль человека в группе. Один человек может учиться в одной группе и вести другую.</summary>
    public enum MembershipKind
    {
        Learner = 0,
        Instructor = 1
    }
}
