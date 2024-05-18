using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class loadSaves : MonoBehaviour
{
    private List<string> saves;
    // Start is called before the first frame update
    void Start()
    {
        //saves = System.IO.Directory.EnumerateFiles("Assets\\Saves").ToList();
        saves = System.IO.Directory.EnumerateDirectories("Assets\\Saves").ToList();
        
        foreach (var save in saves)
        {
            string save2 = save.Split("\\")[2];
            gameObject.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(save2));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
