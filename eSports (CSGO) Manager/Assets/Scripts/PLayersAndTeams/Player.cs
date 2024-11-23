using System;
using UnityEngine.Diagnostics;

[Serializable]
public class Player : TeamMember
{
    public double KillsPerRound { get; set; }
    public double HeadshotPercentage { get; set; }
    public double MapsPlayed { get; set; }
    public double DamagePerRound { get; set; }
    public double RoundsContributed { get; set; }

    public double Rating { get; set; }

    public double Firepower { get; set; } = UnityEngine.Random.Range(0f, 100f);
    public double Entrying { get; set; } = UnityEngine.Random.Range(0f, 100f);
    public double Trading { get; set; } = UnityEngine.Random.Range(0f, 100f);
    public double Opening { get; set; } = UnityEngine.Random.Range(0f, 100f);
    public double Clutching { get; set; } = UnityEngine.Random.Range(0f, 100f);
    public double Sniping { get; set; } = UnityEngine.Random.Range(0f, 100f);
    public double Utility { get; set; } = UnityEngine.Random.Range(0f, 100f);

    public double Health { get; set; } = 100;


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
    public Player(Player player)
    {
        MemberID = player.MemberID;
        InGameName = player.InGameName;
        RealName = player.RealName;
        DateOfBirth = player.DateOfBirth;
        CountryID = player.CountryID;
        MemberTeamID = player.MemberTeamID;

        KillsPerRound = player.KillsPerRound;
        HeadshotPercentage = player.HeadshotPercentage;
        MapsPlayed = player.MapsPlayed;
        DamagePerRound = player.DamagePerRound;
        RoundsContributed = player.RoundsContributed;
        Rating = player.Rating;
        Firepower = player.Firepower;
        Entrying = player.Entrying;
        Trading = player.Trading;
        Opening = player.Opening;
        Clutching = player.Clutching;
        Sniping = player.Sniping;
        Utility = player.Utility;
    }
}
