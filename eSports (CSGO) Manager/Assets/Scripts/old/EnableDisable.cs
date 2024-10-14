using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisable : MonoBehaviour
{

    public GameObject enable;
    public GameObject disable;

    public void onClick()
    {
        Debug.Log("here");
        enable.SetActive(true);
        disable.SetActive(false);
    }
    
}
