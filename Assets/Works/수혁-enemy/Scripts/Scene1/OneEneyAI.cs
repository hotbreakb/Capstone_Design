using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneEneyAI : MonoBehaviour
{
        public enum State{
        MOVE,
        ATTACK,
        DIE
    }
    private Transform playerTr;
    private Transform enemyTr;

    public bool isDie = false;
    
    private WaitForSeconds ws;
    public State state = State.MOVE;
    private Animator animator;
    

    private OneEnmeyFire enmeyFire;


    private readonly int hashMove = Animator.StringToHash("canMove");
    private readonly int hashSpeed = Animator.StringToHash("speed");
    private OneMoveAgent moveAgent;
    private void Awake(){
        var player = GameObject.Find("TmpPlayer");

        if(player != null) playerTr = player.GetComponent<Transform>();

        enemyTr = GetComponent<Transform>();
        ws = new WaitForSeconds(0.3f);
        animator = GetComponent<Animator>();
        moveAgent = GetComponent<OneMoveAgent>();
        enmeyFire = GetComponent<OneEnmeyFire>();
    }

    private void OnEnable(){
        StartCoroutine(Action());
    }

    IEnumerator Action(){
        while(!isDie){
            yield return ws;
            // 상태에 따라 분기처리
            switch(state){
                case State.MOVE:
                    // 총알발사정지
                    //enemyFire.isFire = false;
                    //moveAgent.patrolling = true;
                    animator.SetBool(hashMove, true);

                    break;

                case State.ATTACK:

                    animator.SetBool(hashMove,false);
                    //moveAgent.Stop();
                    if(enmeyFire.isFire == false) enmeyFire.isFire = true;
                    break;
            
            
                   
                case State.DIE:
                    this.gameObject.tag = "Untagged";
                    isDie = true;
                    //enemyMeleeAttack.isMeleeAttack = false;                    
                    //moveAgent.Stop();
                    //int ran = Random.Range(0,3);
                    //animator.SetInteger(hashDieIdx,ran);
                    //animator.SetTrigger(hashDie);
                    //GetComponent<CapsuleCollider>().enabled = false;
                    //Destroy(gameObject,DieDelayTime);
                    break;
            }
        }
    }
    void Update(){
        animator.SetFloat(hashSpeed, moveAgent.speed);
    }
}
