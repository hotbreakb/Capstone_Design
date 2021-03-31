using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starttext : MonoBehaviour
{
    public GameObject StartUI;
    public GameObject Timebar;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(gameStart());
    }

    IEnumerator gameStart()
    {

        yield return new WaitForSeconds(5f);
        StartUI.SetActive(false);
        Timebar.SetActive(true);
    }
 
}
