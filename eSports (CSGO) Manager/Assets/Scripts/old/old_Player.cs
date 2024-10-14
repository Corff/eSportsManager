using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class old_Player : MonoBehaviour
{
    public string ign = "";
    public string realName = "";
    public string country = "";
    public int age = 0;
    public string team = "";
    public string rating2 = "";
    public string kpr = "";
    public string headshot = "";
    public string mapsPlayed = "";
    public string dpr = "";
    public string roundsContributed = "";

    public void init(string row)
    {
        string[] splitRow = row.Split(',');
        this.ign = splitRow[0];
        this.realName = splitRow[1];
        this.country = splitRow[2];
        this.age = int.Parse(splitRow[3]);
        this.team = splitRow[4];
        this.rating2 = splitRow[5];
        this.kpr = splitRow[6];
        this.headshot = splitRow[7];
        this.mapsPlayed = splitRow[8];
        this.dpr = splitRow[9];
        this.roundsContributed = splitRow[10];
        
    }

}