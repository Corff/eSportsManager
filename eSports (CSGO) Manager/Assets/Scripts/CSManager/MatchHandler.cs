using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Enums;
using UnityEngine.SceneManagement;

public class MatchHandler : MonoBehaviour
{
    #region UI

    public TMP_Text team1Text;
    public TMP_Text team2Text;
    
    #endregion
    
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
        while (t1Index == t2Index);
        Team t1 = DataHolder.Teams[t1Index];
        Team t2 = DataHolder.Teams[t2Index];
        me = new(t1, t2);
        team1Text.text = t1.TeamName;
        team2Text.text = t2.TeamName;
    }
    
    public void StartSeries(int mapCount)
    {
        me.SimulateMatch(mapCount);
    }
}
