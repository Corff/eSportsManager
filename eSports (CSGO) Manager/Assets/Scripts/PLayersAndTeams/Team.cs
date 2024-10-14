using System;
using System.Collections.Generic;

[Serializable]
public class Team
{
    public Guid TeamID { get; set; } = Guid.NewGuid();
    public string TeamName { get; set; }

    public Coach Coach { get; set; }
    public List<Player> Players { get; set; }

    public Team() { }
    public Team(string teamName)
    {
        TeamName = teamName;
    }
}
