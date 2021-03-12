using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadeout : MonoBehaviour
{
    public Image Background;
    public Camera gunCamera;
    Vector3 cameraPos;
    public float fadeOutTime = 1.0f;

    [SerializeField] [Range(0.01f, 0.1f)] float shakeRange = 0.05f;
    [SerializeField] [Range(0.1f, 2f)] float duration = 2f;

    private void Start()
    {
        //Shake();
        cameraPos = gunCamera.transform.localPosition;
        

    }
    private void Update()
    {
        StartCoroutine("FadeOut");
        Shake(shakeRange, duration);
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);
        Color tempColor = Background.color;
        while (tempColor.a < 1f)
        {
            tempColor.a += Time.deltaTime / fadeOutTime;
            Background.color = tempColor;

            if (tempColor.a >= 1f) tempColor.a = 1f;

            yield return null;
        }
  
            //num = color.a;
            //color.a += Time.deltaTime * 0.005f;
            //Background.color = color;
    }

    /*void Shake()
    {
        cameraPos = gunCamera.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
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
    }*/

    public IEnumerator Shake(float _amount, float _duration)
    {
        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + cameraPos;
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = cameraPos;
    }
}
