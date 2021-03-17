using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadeout : MonoBehaviour
{
    public GameObject gunCamera;
    Vector3 cameraPos;

    [SerializeField] [Range(0.01f, 0.3f)] float shakeRange = 0.05f;
    [SerializeField] [Range(0.1f, 4f)] float duration = 3f;

   
    void Shake()
    {
        cameraPos = gunCamera.transform.position;
        InvokeRepeating("StartShake", 0f, 0.009f);
        Invoke("StopShake", duration);
    }

    void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = gunCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        gunCamera.transform.position = cameraPos;
    }

    void StopShake()
    {
        CancelInvoke("StartShake");
        gunCamera.transform.position = cameraPos;
    }

}
