using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using TMPro;

public class TournamentHandler : MonoBehaviour
{
    private string playerTeamName;
    private string saveName;

    private string[][] teamList;
    private List<string> actualTeamList;

    public Image teamLogo;

    public Transform teamTexts;

    private Dictionary<string, int[]> teamAndRecords;

    public GameObject teamObjectsParent;

    // Start is called before the first frame update
    void Start()
    {
        saveName = System.IO.File.ReadAllText("Assets/Saves/currentSave.txt");
        playerTeamName = System.IO.File.ReadAllText("Assets/Saves/" + saveName + "/teamSelected.txt");
        loadTeamLogo(playerTeamName);
    }

    void loadTeamLogo(string teamName)
    {
        teamLogo.sprite = Resources.Load<Sprite>("Team Logos/" + teamName);
    }

    public void startTournament()
    {
        List<string> competingTeams = chooseTeams();
        updateTeamList(competingTeams);
        foreach (string team in competingTeams)
        {
            GameObject t = new GameObject(team);
            t.transform.parent = teamObjectsParent.transform;
            t.AddComponent<old_Team>(); 
            //GameObject.Instantiate(t, teamObjectsParent.transform);
            //Instantiate(t, teamObjectsParent.transform);
        }
    }

    List<string> chooseTeams()
    {
        List<string> t = new();
        t.Add(playerTeamName);
        string path = "Assets/Lists/playersTeamsAndStats.csv";
        teamList = File.ReadLines(path).Select(x => x.Split(',')).ToArray();
        actualTeamList = new List<string>();
        foreach (string[] line in teamList)
        {
            if (actualTeamList.Contains(line[4]) == false && line[4].Contains("(benched)") == false &&
                line[4] != "No team")
            {
                actualTeamList.Add(line[4]);
            }
        }

        int[] count = new int[actualTeamList.Count];
        //Remove teams if they dont have 5 players
        for (int i = 0; i < actualTeamList.Count; i++)
        {
            for (int j = 0; j < teamList.Length; j++)
            {
                if (actualTeamList[i] == teamList[j][4])
                {
                    count[i]++;
                }
            }
        }

        for (int i = 0; i < 15; i++)
        {
            bool added = false;
            while (added == false)
            {
                int randomIndex = Random.Range(0, actualTeamList.Count);
                if (t.Contains(actualTeamList[randomIndex]) == false)
                {
                    if (count[randomIndex] == 5)
                    {
                        t.Add(actualTeamList[randomIndex]);
                        added = true;
                    }
                    
                }
            }
        }

        initRecords(t);

        return t;
    }

    void initRecords(List<string> teams)
    {
        teamAndRecords = new();
        for (int i = 0; i < teams.Count; i++)
        {
            teamAndRecords.Add(teams[i], new int[] { 0, 0 });
        }
    }

    void updateTeamList(List<string> t)
    {
        List<Transform> children = new();
        foreach (Transform child in teamTexts)
        {
            children.Add(child);
        }

        for (int i = 0; i < t.Count; i++)
        {
            List<Transform> c2 = new();
            foreach (Transform child in children[i])
            {
                c2.Add(child);
            }

            c2[0].gameObject.GetComponent<TMP_Text>().text = t[i];
            c2[1].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Team Logos/" + t[i]);
        }



        //for (int i = 0; i < t.Count; i++)
        //{
        //    List<Transform> children2 = new();
        //    foreach (Transform child2 in children)
        //    {
        //        children2.Add(child2);
        //    }
        //    children2[0].gameObject.GetComponent<TMP_Text>().text = t[i];
        //    children2[1].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Team Logos/" + t[i]);
        //}
    }

    public void generateFixtures()
    {
        //generate fixtures for each team in teamsAndRecords, against teams with the same Values
        
    }
}