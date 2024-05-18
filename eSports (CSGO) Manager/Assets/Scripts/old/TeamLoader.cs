using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TeamLoader : MonoBehaviour
{

    //public TextAsset textFile;

    public string[] teamList;

    public TextMeshProUGUI team1Name;
    public TextMeshProUGUI T1P1;
    public TextMeshProUGUI T1P2;
    public TextMeshProUGUI T1P3;
    public TextMeshProUGUI T1P4;
    public TextMeshProUGUI T1P5;
    
    public TextMeshProUGUI team2Name;
    public TextMeshProUGUI T2P1;
    public TextMeshProUGUI T2P2;
    public TextMeshProUGUI T2P3;
    public TextMeshProUGUI T2P4;
    public TextMeshProUGUI T2P5;

    private string[][] playerList;

    string[] chooseRandomTeams()
    {
        int numOfTeams = teamList.Length;
        string[] teams = new string[2];
        bool same = true;
        while (same)
        {
            teams[0] = teamList[Random.Range(0, numOfTeams)];
            teams[1] = teamList[Random.Range(0,numOfTeams)];
            if (teams[0] == teams[1])
            {
                same = true;
            }
            else
            {
                same = false;
            }
        }
        return teams;
    }

    void loadTeamsByID(string name1, string name2)
    {

        //string[] t1;
        //string[] t2;
        List<string> t1 = new List<string>();
        List<string> t2 = new List<string>();
        for (int i = 0; i < playerList.Length; i++)
        {
            if (playerList[i][1] == name1)
            {
                t1.Add(playerList[i][0]);
            }
            else if (playerList[i][1] == name2)
            {
                t2.Add(playerList[i][0]);
            }
        }

        team1Name.text = name1;
        T1P1.text = t1[0];
        T1P2.text = t1[1];
        T1P3.text = t1[2];
        T1P4.text = t1[3];
        T1P5.text = t1[4];
        
        team2Name.text = name2;
        T2P1.text = t2[0];
        T2P2.text = t2[1];
        T2P3.text = t2[2];
        T2P4.text = t2[3];
        T2P5.text = t2[4];


    }

    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/Lists/players.csv";
        playerList = File.ReadLines(path).Select(x => x.Split(',')).ToArray();

        string[] teams = chooseRandomTeams();
        
        loadTeamsByID(teams[0], teams[1]);
        


    }
    

}
