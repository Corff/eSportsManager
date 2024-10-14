using System;
using UnityEngine.Diagnostics;

[Serializable]
public class Player : TeamMember
{
    public double Rating { get; set; }
    public double KillsPerRound { get; set; }
    public double HeadshotPercentage { get; set; }
    public double MapsPlayed { get; set; }
    public double DamagePerRound { get; set; }
    public double RoundsContributed { get; set; }

    public Player() { }
    public Player(string[] data)
    {

        InGameName = data[0];
        RealName = data[1];
        DateOfBirth = DateTime.Now;
        Rating = double.Parse(data[5]);
        KillsPerRound = double.Parse(data[6]);
        MapsPlayed = double.Parse(data[8]);
        DamagePerRound = double.Parse(data[9]);
        RoundsContributed = double.Parse(data[10]);
    }
}
