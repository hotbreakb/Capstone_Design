using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class PlayStart2 : MonoBehaviour
{
    

    [Header("Changes")]
    public float changeTime = 20.0f;
    public GameObject Player;
    public GameObject bullet;
    public GameObject bulletCase;
    public GameObject grenade;
    public GameObject Handgun;
    Animator anim;

     [Header("Windows")]
     public GameObject[] Windows;

    [Header("Props")]
    public GameObject Props;
    public GameObject messProps;
    public GameObject Labtop;
    public GameObject newLabtop;
    public GameObject table;
    public GameObject newtable;


    [Header("Bomb effect")]
    public GameObject Bombs;
    public GameObject Fires;

    [Header("UI")]
    public GameObject UI;
    public GameObject labtopBar;
    public GameObject WordsUI;
    public GameObject Rule;

    //UI
    [Header("Eyeblink")]
    public GameObject Eyeblinkpanel;

    [Header("Fade Out")]
    public Image Background;
    public float fadeOutTime = 1.0f;

    [Header("Shake Camera")]
    public GameObject Canvas;
    public GameObject GunCamera;

    [Header("Enemy")]
    public GameObject enemySpawn;

    [Header("PlayerMoveAgent")]
    public GameObject goal;
    private Transform goal_t;
    private Transform player_t;
    public float damping;
    public bool isMove;

    [Header("Audio")]
    public GameObject AudioManager;

    private void Start()
    {
        Handgun.GetComponent<HandgunScriptLPFP>().enabled = false;
        goal_t = goal.GetComponent<Transform>();
        player_t = Player.GetComponent<Transform>();
        anim.SetBool("Holster", true);
        labtopBar.SetActive(false);
        Background.enabled = true;
        StartCoroutine(delay());
        isMove = false;
    }

    private void Awake()
    {
        anim = Handgun.GetComponent<Animator>();

    }

    private void Update()
    {
        if (isMove == true)
        {
            anim.SetBool("Run", true);
            player_t.position = Vector3.Lerp(player_t.position, goal_t.position, 0.05f);
            if(player_t.position == goal_t.position)
            {
                isMove = false;
                anim.SetBool("Run", false);
            }
        }
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
        bullet.transform.localScale = new Vector3(0.004f, 0.004f, 0.004f);
        bulletCase.transform.localScale = new Vector3(0.11f, 0.11f, 0.11f);
        Labtop.transform.localPosition = new Vector3(1.79f, -1.47f, -0.17f);
        Labtop.transform.localRotation = Quaternion.Euler(new Vector3(0f, -38.419f, 0f));
        table.transform.localPosition = new Vector3(-2.18f, -0.1561772f, -0.119f);
        table.transform.localRotation = Quaternion.Euler(new Vector3(-65.07f, 66.911f, 69.79401f));
        grenade.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        



    }
    IEnumerator delay()
    {

        //yield return StartCoroutine(ActWordsUI());
        yield return StartCoroutine(ActInfoUI());
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
    IEnumerator ActWordsUI()
    {
        yield return new WaitForSeconds(3f);
        WordsUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        WordsUI.SetActive(false);
    }
    

    IEnumerator PlayerMoveAgent()
    {
        
        isMove = true;
        
        yield return new WaitForSeconds(0f);
        if (isMove == false)
        {
            
            yield return null;
        }

    }
    IEnumerator ActInfoUI()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(PlayerMoveAgent());
        //yield return new WaitForSeconds(1f);
        //Rule.SetActive(true);
        //yield return new WaitForSeconds(10.5f);
        //Rule.SetActive(false);
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
        Labtop.SetActive(false);
        table.SetActive(false);
        newLabtop.SetActive(true);
        newtable.SetActive(true);
        messProps.SetActive(true);
    }
    IEnumerator FireAct()
    {
        yield return new WaitForSeconds(0f);
        Fires.SetActive(true);
        brakeWindowinStart();
        
    }
    IEnumerator BombAct()
    {
        yield return new WaitForSeconds(2f);
        Bombs.SetActive(true);
        Fires.SetActive(false);
        Canvas.SendMessage("Shake");
        AudioManager.SetActive(true);

    }
    void BombnotAct()
    {
        Bombs.SetActive(false);
    }

    IEnumerator UIAct()
    {
        yield return new WaitForSeconds(Time.deltaTime + 3f);
        anim.SetBool("Holster", false);
        Handgun.GetComponent<HandgunScriptLPFP>().enabled = true;
        UI.SetActive(true);
        enemySpawn.SetActive(true);
        labtopBar.SetActive(true);
    }

    void brakeWindowinStart(){
        // yield return new WaitForSeconds(4f);
        for(int i=0;i<Windows.Length;i++){
            Windows[i].GetComponent<breakWindow>().breakAuto();
        }
    }
}
