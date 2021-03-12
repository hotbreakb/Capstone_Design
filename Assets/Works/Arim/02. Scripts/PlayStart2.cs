using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayStart2 : MonoBehaviour
{

    [Header("Change Size")]
    public float changeTime = 20.0f;
    public GameObject Player;
    public GameObject TmpPlayer;
    public GameObject Enemy;
    public GameObject bullet;
    public GameObject bulletCase;
    public GameObject desk;
    public GameObject grenade;

    [Header("Change Props")]
    public GameObject Props;
    public GameObject messProps;


    [Header("Bomb effect")]
    public GameObject Bombs;

    [Header("UI")]
    public GameObject UI;

    //UI
    [Header("Eyeblink")]
    public GameObject Eyeblinkpanel;

    [Header("Fade Out")]
    public Image Background;
    public float fadeOutTime = 1.0f;

    [Header("Shake Camera")]
    public Camera gunCamera;
    Vector3 cameraPos;
    [SerializeField] [Range(0.01f, 0.1f)] float shakeRange = 0.05f;
    [SerializeField] [Range(0.1f, 2f)] float duration = 2f;


    private void Start()
    {
        Background.enabled = true;
        //Shake();
        //StartCoroutine(FadeOut());
        StartCoroutine(delay());
        cameraPos = gunCamera.transform.localPosition;


    }
    private void Update()
    {
        //StartCoroutine(FadeOut());
        //StartCoroutine(delay());
        //StartCoroutine("FadeOut");
        //Shake(shakeRange, duration);
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
        yield return new WaitForSeconds(0.3f);

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

    /*public IEnumerator Shake(float _amount, float _duration)
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
    */
    // Update is called once per frame

    void Changes()
    {
        Player.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        Enemy.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        TmpPlayer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        bullet.transform.localScale = new Vector3(0.004f, 0.004f, 0.004f);
        bulletCase.transform.localScale = new Vector3(0.11f, 0.11f, 0.11f);
        desk.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        desk.transform.position = new Vector3(0.339f, -1.53f, desk.transform.position.z);
        //labtop.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        grenade.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);


    }
    IEnumerator delay()
    {
        yield return StartCoroutine(FadeOut());
        yield return StartCoroutine("BombAct");
        yield return StartCoroutine("EyeblinkNotAct");
        yield return StartCoroutine("PropsAct");
        yield return StartCoroutine("Changes");
        yield return StartCoroutine("EyeblinkActive");
        yield return StartCoroutine(UIAct());
    }
    void EyeblinkActive()
    {
        Background.enabled = false;
        Eyeblinkpanel.SetActive(true);
    }
    void EyeblinkNotAct()
    {
        Eyeblinkpanel.SetActive(false);
    }
    void PropsAct()
    {
        Props.SetActive(false);
        messProps.SetActive(true);
    }
    void BombAct()
    {
        Bombs.SetActive(false);
    }

    IEnumerator UIAct()
    {
        yield return new WaitForSeconds(Time.deltaTime + 3f);
        UI.SetActive(true);
    }
}
