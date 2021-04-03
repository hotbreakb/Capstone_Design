using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneAutoInStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<LoadSceneAuto>().LoadMenu();  
    }
}
