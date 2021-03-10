using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStart2 : MonoBehaviour
{
    public float changeTime = 20.0f;
    public GameObject Player;
    public GameObject TmpPlayer;
    public GameObject Enemy;
    public GameObject bullet;
    public GameObject bulletCase;
    public GameObject desk;
    //public GameObject labtop;
    public GameObject grenade;
    public GameObject Props;
    public GameObject messProps;


    //UI
    public GameObject Eyeblinkpanel;



    // Start is called before the first frame update
    void Start()
    {
        //Props.SetActive(true);
        //messProps.SetActive(false);
        StartCoroutine(delay());
        
    }

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
        yield return StartCoroutine("EyeblinkNotAct");
        yield return StartCoroutine("PropsAct");
        yield return StartCoroutine("Changes");
        yield return StartCoroutine("EyeblinkActive");
    }
    void EyeblinkActive()
    {
        Eyeblinkpanel.SetActive(true);
    }
    void EyeblinkNotAct()
    {
        Eyeblinkpanel.SetActive(false);
    }
    void PropsAct() {
        Props.SetActive(false);
        messProps.SetActive(true);
    }
}
