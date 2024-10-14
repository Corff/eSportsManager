using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;
using UnityEngine.SceneManagement;

public class MatchHandler : MonoBehaviour
{
    //public Team team1;
    //public Team team2;
    MatchEngine me;
    private void Awake()
    {
        if (!DataHolder.Loaded)
        {
            SceneManager.LoadScene((int)SceneID.LoadingScene);
        }
    }

    private void Start()
    {
        int t1Index = UnityEngine.Random.Range(0, DataHolder.Teams.Count);
        int t2Index;
        do
        {
            t2Index = UnityEngine.Random.Range(0, DataHolder.Teams.Count);
        }
        while (t1Index != t2Index);
        Team t1 = DataHolder.Teams[t1Index];
        Team t2 = DataHolder.Teams[t2Index];
        me = new(t1, t2);
    }
}
