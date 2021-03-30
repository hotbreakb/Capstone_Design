using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneAutoinStart : MonoBehaviour
{
    void Start()
    {
        GetComponent<LoadSceneAuto>().LoadMenu();  
    }
}
