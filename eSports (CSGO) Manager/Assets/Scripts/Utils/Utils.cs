using System;
using UnityEngine;
using static Enums;

public static class Utils
{
    public static DateTime GetYearOfBirth(int age)
    {
        DateTime dt = new(2024,1,1);
        dt = dt.AddYears(-age);
        return dt;
    }

    public static string PlayerData
    {
        get => PlayerPrefs.GetString(PlayerPrefsKeys.PlayersData);
        set => PlayerPrefs.SetString(PlayerPrefsKeys.PlayersData, value);
    }
    public static string TeamsData
    {
        get => PlayerPrefs.GetString(PlayerPrefsKeys.TeamsData);
        set => PlayerPrefs.SetString(PlayerPrefsKeys.TeamsData, value);
    }
}
