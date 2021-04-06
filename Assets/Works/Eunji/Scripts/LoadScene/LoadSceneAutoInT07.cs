using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneAutoInT07 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadingStart());
    }

    IEnumerator loadingStart(){
        yield return new WaitForSecondsRealtime(3.5f);
        GetComponent<LoadSceneAuto>().LoadingStart();
    }
}
