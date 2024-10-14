using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class old_Team : MonoBehaviour
{
    private string teamName;

    private old_Player p1;
    private old_Player p2;
    private old_Player p3;
    private old_Player p4;
    private old_Player p5;
    
    // Start is called before the first frame update
    public void Start()
    {
        //gameObject.name = teamName;
        teamName = gameObject.name;
        string[] allPlayerRows = Resources.Load<TextAsset>("Lists/playersTeamsAndStats").text.Split("\r\n");
        List<string> teamPlayerRows = new();
        int len = allPlayerRows.Length-1;
        for (int i = 0; i < len; i++)
        {
            if (allPlayerRows[i] == "")
            {
                continue;
            }
            string gv = getVal(allPlayerRows[i], 4);
            if (gv == teamName)
            {
                teamPlayerRows.Add(allPlayerRows[i]);
            }
            if(teamPlayerRows.Count == 5)
                break;
        }

        var c1 = gameObject.AddComponent<old_Player>();
        c1.init(teamPlayerRows[0]);
        var c2 = gameObject.AddComponent<old_Player>();
        c2.init(teamPlayerRows[1]);
        var c3 = gameObject.AddComponent<old_Player>();
        c3.init(teamPlayerRows[2]);
        var c4 = gameObject.AddComponent<old_Player>();
        c4.init(teamPlayerRows[3]);
        var c5 = gameObject.AddComponent<old_Player>();
        c5.init(teamPlayerRows[4]);

    }

    string getVal(string row, int index)
    {
        string[] rowSplit = row.Split(",");
        return row.Split(",")[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
