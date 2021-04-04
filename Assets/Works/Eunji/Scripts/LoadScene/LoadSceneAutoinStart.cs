using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneAutoInStart : MonoBehaviour
{
    void Start()
    {
        GetComponent<LoadSceneAuto>().LoadMenu();  
    }
}
