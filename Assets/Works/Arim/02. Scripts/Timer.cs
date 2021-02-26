using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeSpeed = 0.01f;
    private float totalTime = 0f;
    public Image progressBar;

    void Start()
    {
    }


    void Update()
    {
        totalTime += Time.deltaTime;
        progressBar.fillAmount = totalTime * timeSpeed;


    }
  
}
