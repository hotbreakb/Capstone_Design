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
    public GameObject Fires;

    [Header("UI")]
    public GameObject UI;

    //UI
    [Header("Eyeblink")]
    public GameObject Eyeblinkpanel;

    [Header("Fade Out")]
    public Image Background;
    public float fadeOutTime = 1.0f;

    [Header("Shake Camera")]
    public GameObject Canvas;
    public GameObject GunCamera;


    private void Start()
    {
        Background.enabled = true;
        StartCoroutine(delay());


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

     
    }

   
    void Changes()
    {
        Player.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        Enemy.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        TmpPlayer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        bullet.transform.localScale = new Vector3(0.004f, 0.004f, 0.004f);
        bulletCase.transform.localScale = new Vector3(0.11f, 0.11f, 0.11f);
        desk.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        desk.transform.position = new Vector3(0.339f, -1.53f, desk.transform.position.z);
        grenade.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        



    }
    IEnumerator delay()
    {
        yield return StartCoroutine(FireAct());
        yield return StartCoroutine(BombAct());
        yield return StartCoroutine(FadeOut());
        yield return StartCoroutine("BombnotAct");
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
    IEnumerator FireAct()
    {
        yield return new WaitForSeconds(4f);
        Fires.SetActive(true);
        
    }
    IEnumerator BombAct()
    {
        yield return new WaitForSeconds(2f);
        Bombs.SetActive(true);
        Fires.SetActive(false);
        Canvas.SendMessage("Shake");
    }
    void BombnotAct()
    {
        Bombs.SetActive(false);
    }

    IEnumerator UIAct()
    {
        yield return new WaitForSeconds(Time.deltaTime + 3f);
        UI.SetActive(true);
    }
}
