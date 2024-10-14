using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using static Enums;
using static Utils;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    List<Player> allPlayers = new();
    List<Team> allTeams = new();
    List<string> allTeamNames = new();
    List<Team> finalTeams = new();

    string playersDataFile;
    string teamsDataFile;

    private void Start()
    {
        teamsDataFile = Application.persistentDataPath + "/teams.json";
        playersDataFile = Application.persistentDataPath + "/players.json";
        if (!File.Exists(playersDataFile) &&
            !File.Exists(teamsDataFile))
        {
            CreateDB();
        }
        else
        {
            LoadDB();
            Debug.Log("Loaded existing DB");
        }
        DataHolder.Teams = finalTeams;
        DataHolder.Players = allPlayers;
        DataHolder.Loaded = true;
        SceneManager.LoadScene((int)SceneID.MatchEngine);
    }

    void CreateDB()
    {
        string path = "Assets/Lists/playersTeamsAndStats.csv";
        string[][] playerList = File.ReadLines(path).Select(x => x.Split(',')).ToArray();
        foreach(var p in playerList)
        {
            if (!allTeamNames.Contains(p[4]))
            {
                Team thisTeam = new()
                {
                    TeamName = p[4]
                };
                allTeams.Add(thisTeam);
                allTeamNames.Add(p[4]);
            }
            allPlayers.Add(CreatePlayerObject(p));
        }
        FillTeams();
        SaveDB();
        Debug.Log("Created new DB");
    }

    void SaveDB()
    {
        string teamsSerialized = JsonConvert.SerializeObject(allTeams, Formatting.Indented);
        using (StreamWriter sw = new(teamsDataFile))
        {
            sw.Write(teamsSerialized);
        }
        string playersSerialized = JsonConvert.SerializeObject(allPlayers, Formatting.Indented);
        using (StreamWriter sw = new(playersDataFile))
        {
            sw.Write(playersSerialized);
        }
        PlayerData = playersSerialized;
        TeamsData = teamsSerialized;
    }

    void LoadDB()
    {
        string teamsData = "";
        string playersData = "";
        using (StreamReader sr = new(teamsDataFile))
        {
            teamsData = sr.ReadToEnd();
        }
        using (StreamReader sr = new(playersDataFile))
        {
            playersData = sr.ReadToEnd();
        }
        finalTeams = JsonConvert.DeserializeObject<List<Team>>(teamsData);
        allPlayers = JsonConvert.DeserializeObject<List<Player>>(playersData);
    }

    Player CreatePlayerObject(string[] p)
    {
        if (p.Length != 11) return null;
        for (int i = 0; i < p.Length; i++)
        {
            if (p[i].EndsWith("%"))
            {
                p[i] = p[i].Substring(0, p[i].Length - 1);
            }
        }
        Player thisPlayer = new()
        {
            InGameName = p[0],
            RealName = p[1],
            MemberTeamID = GetTeamID(p[4]),
            DateOfBirth = GetYearOfBirth(int.Parse(p[3])),
            Rating = double.Parse(p[5]),
            KillsPerRound = double.Parse(p[6]),
            HeadshotPercentage = double.Parse(p[7]),
            MapsPlayed = double.Parse(p[8]),
            DamagePerRound = double.Parse(p[9]),
            RoundsContributed = double.Parse(p[10])
        };
        return thisPlayer;
    }

    Guid GetTeamID(string teamName)
    {
        foreach(var team in allTeams)
        {
            if(team.TeamName == teamName)
            {
                return team.TeamID;
            }
        }
        return Guid.Empty;
    }

    void FillTeams()
    {
        foreach(var team in allTeams)
        {
            List<Player> thisTeamsPlayers = allPlayers.Where(i => i.MemberTeamID == team.TeamID).ToList();
            if (thisTeamsPlayers.Count != 5) continue;
            team.Players = thisTeamsPlayers;
            finalTeams.Add(team);

        }
    }
}
