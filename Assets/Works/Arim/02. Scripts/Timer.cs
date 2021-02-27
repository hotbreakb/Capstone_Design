﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeSpeed = 0.01f;
    private float totalTime = 0f;
    private float fills;
    private float bt = 0.1f;
    public Image progressBar;
    public GameObject Spotlight;
    public GameObject Pointlight;
    public Light Fleshlight;
    public float delayTime = 40.0f;
    public bool lightmanager = false;

    void Start()
    {
        Fleshlight.enabled = false;
    }


    void Update()
    {
        if (fills >= 0.4f && lightmanager == false) {
            LightManager();
        }
            
        totalTime += Time.deltaTime;
        fills = totalTime * timeSpeed;
        progressBar.fillAmount = fills;


    }
    void LightManager()
    {
        lightmanager = true;
        StartCoroutine(Blinking());
        StartCoroutine(Blinktime());
        bt += 0.2f;
        StartCoroutine(Blinking());
        StartCoroutine(Blinktime());
        bt += 0.6f;
        StartCoroutine(Blinking());
        StartCoroutine(FleshLightOn());
        StartCoroutine(WaitForIt());
    }
    IEnumerator Blinking()
    {
        yield return new WaitForSeconds(bt);
        Spotlight.SetActive(false);
        Pointlight.SetActive(false);
    }
    IEnumerator FleshLightOn()
    {
        yield return new WaitForSeconds(1.2f);
        Fleshlight.enabled = true;

    }
    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(delayTime);
        Fleshlight.enabled = false ;
        Spotlight.SetActive(true);
        Pointlight.SetActive(true);
    }
    IEnumerator Blinktime()
    {
        yield return new WaitForSeconds(bt*2);
        Spotlight.SetActive(true);
        Pointlight.SetActive(true);
    }

}
