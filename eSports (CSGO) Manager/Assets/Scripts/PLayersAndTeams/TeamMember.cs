using System;

[Serializable]
public class TeamMember
{
    public Guid MemberID { get; set; } = Guid.NewGuid();
    public string InGameName { get; set; }
    public string RealName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public int CountryID { get; set; }
    public Guid MemberTeamID { get; set; }
}
