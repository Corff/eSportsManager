using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/Lists/playersTeamsAndStats.csv";
        var teamList = File.ReadLines(path).Select(x => x.Split(',')).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
