using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SC2PlayerDamage : MonoBehaviour
{
 
    private float initHp = 200.0f;
    private Color currColor;
    private readonly Color initColor = new Vector4(0, 1.0f, 0.0f, 1.0f);
    private float currHP;

    public Image bloodScreen;
    public Image hpBar;

    [Header("HPItem")]
    public bool hpitem = false;
    public GameObject HPItem;
    public float destroyTime = 10f;
    //public delegate void PlayerDieHandler();
    //public static event PlayerDieHandler OnPlayerDie;



    private void OnCollisionEnter(Collision collision){
        if(collision.transform.tag =="SC2EnemyBullet" || collision.transform.tag =="Enemy"){
            Debug.Log(collision.transform.tag);
            Debug.Log(currHP);
            StartCoroutine(ShowBloodScreen());
            currHP -= 5.0f;
            DisPlayHpbar();
            if (currHP <= 0.0f) //플레이어 죽었을때
            {
                PlayerDie();
            }
        }
    }

        public void AttackedByMelee(){
            StartCoroutine(ShowBloodScreen());
            currHP -= 5.0f;
            DisPlayHpbar();
            if (currHP <= 0.0f) //플레이어 죽었을때
            {
                PlayerDie();
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

        hpBar.color = initColor;
        currColor = initColor;
    }

    // Update is called once per frame
    void Update()
    {
        HpItem();
    }

    IEnumerator ShowBloodScreen()
    {
        bloodScreen.color = new Color(1, 0, 0,UnityEngine.Random.Range(0.2f, 0.3f));
        yield return new WaitForSeconds(0.1f);
        bloodScreen.color = Color.clear;
    }

    void DisPlayHpbar()
    {
        if ((currHP / initHp) > 0.5f)
            currColor.r = (1 - (currHP / initHp)) * 2.0f;
        else
            currColor.g = (currHP / initHp) * 2.0f;
        hpBar.color = currColor;
        hpBar.fillAmount = (currHP / initHp);
    }
    public void Heal()
    {
        Debug.Log("Heal~~~~~~~~~~~~~~");
        currHP += 50f;
        AudioSource sound = GetComponent<AudioSource>();
        sound.Play();

    }
    IEnumerator DestroyHpItem()
    {
        yield return new WaitForSeconds(destroyTime);
        HPItem.SetActive(false);
    }
    void HpItem()
    {
        if ((hpitem == false) && (currHP == 50))
        {
            HPItem.SetActive(true);
            hpitem = true;
            StartCoroutine(DestroyHpItem());
        }
    }
}

