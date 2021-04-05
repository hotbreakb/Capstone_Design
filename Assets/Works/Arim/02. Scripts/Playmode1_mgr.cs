using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playmode1_mgr : MonoBehaviour
{
    public GameObject WordsUI;
    public GameObject RuleUI;
    public GameObject Timebar;
    public GameObject UI;
    public GameObject Handgun;
    public GameObject Player;
    public GameObject StartUI;
    public GameObject EventSystem;
    Animator anim;


    [Header("PlayerMoveAgent")]
    public GameObject goal;
    private Transform goal_t;
    private Transform player_t;
    public float damping;
    public bool isMove;

    private void Start()
    {
        Handgun.GetComponent<HandgunScriptLPFP>().enabled = false;
        goal_t = goal.GetComponent<Transform>();
        player_t = Player.GetComponent<Transform>();
        anim.SetBool("Holster", true);
        StartCoroutine(Delay());
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
            if (player_t.position == goal_t.position)
            {
                isMove = false;
                anim.SetBool("Run", false);
            }
        }
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
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        WordsUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        StartCoroutine(PlayerMoveAgent());
        yield return new WaitForSeconds(3f);
        WordsUI.SetActive(false);
        RuleUI.SetActive(true);
        yield return new WaitForSeconds(7.5f);
        RuleUI.SetActive(false);
        StartUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        StartUI.SetActive(false);
        Timebar.SetActive(true);
        UI.SetActive(true);
        anim.SetBool("Holster", false);
        Handgun.GetComponent<HandgunScriptLPFP>().enabled = true;
        EventSystem.GetComponent<GameManager>().enabled = true;

    }
}
