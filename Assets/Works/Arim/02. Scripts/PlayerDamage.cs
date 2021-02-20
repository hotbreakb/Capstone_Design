using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerDamage : MonoBehaviour
{
    private const string bulletTag = "Bullet";
    private float initHp = 100.0f;
    public float currHP;

    public Image bloodScreen;
    //public delegate void PlayerDieHandler();
    //public static event PlayerDieHandler OnPlayerDie;

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == bulletTag)
        {
            StartCoroutine(ShowBloodScreen());
            currHP -= 5.0f;
            Debug.Log("PlayerHP = " + currHP.ToString());

            if (currHP <= 0.0f) //플레이어 죽었을때
            {
                PlayerDie();
            }
        }
    }

    private void PlayerDie()
    {
        Debug.Log("Player Die!!!");
    }
    // Start is called before the first frame update
    void Start()
    {
        currHP = initHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowBloodScreen()
    {
        bloodScreen.color = new Color(1, 0, 0,UnityEngine.Random.Range(0.2f, 0.3f));
        yield return new WaitForSeconds(0.1f);
        bloodScreen.color = Color.clear;
    }
}
