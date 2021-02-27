using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeSpeed = 0.01f;
    private float totalTime = 0f;
    private float fills;
    public Image progressBar;
    public Light Spotlight;
    public Light Pointlight;
    public Light Fleshlight;
    public float delayTime = 10.0f;
    public bool lightmanager = false;

    void Start()
    {
    }


    void Update()
    {
        if (fills >= 0.5f && lightmanager == false) {
            LightManager();
        }
            
        totalTime += Time.deltaTime;
        fills = totalTime * timeSpeed;
        progressBar.fillAmount = fills;


    }
    void LightManager()
    {
        lightmanager = true;
        Fleshlight.enabled = true;
        Spotlight.enabled = false;
        Pointlight.enabled = false;
        StartCoroutine(WaitForIt());
    }
    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(delayTime);
        Fleshlight.enabled = false ;
        Spotlight.enabled = true;
        Pointlight.enabled = true;
    }

}
