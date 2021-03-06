using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{   

    public enum State{
        PATROL,
        TRACE,
        ATTACK,
        DIE
    }


    




    public State state = State.PATROL;


    private Transform playerTr;
    private Transform enemyTr;

    // Animator 컴포넌트 저장할 변수
    private Animator animator;


    public float attackDist = 5.0f;
    public float traceDist = 10.0f;

    public  bool isDie = false;

    private WaitForSeconds ws;


    // 이동을 제어하는 MoveAgent클래스를 저장할 변수
    private MoveAgent moveAgent;


    // 애니메이터 컨트롤러에 정의한 파라미터의 해시값을 미리 추출

    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashSpeed = Animator.StringToHash("Speed");



    private void Awake(){
        var player = GameObject.FindGameObjectWithTag("Player");

        if(player !=null){
            playerTr = player.GetComponent<Transform>();
        }


        enemyTr = GetComponent<Transform>();
        moveAgent = GetComponent<MoveAgent>();
        animator = GetComponent<Animator>();

        ws = new WaitForSeconds(0.3f);


    }

    private void OnEnable(){
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }


    IEnumerator Action(){

        // 적 캐릭터가 사망할 때 까지 무한루프

        while (!isDie){
            yield return ws;

            switch(state){
                case State.PATROL:
                    moveAgent.patrolling = true;
                    animator.SetBool(hashMove, true);
                    break;

                case State.TRACE:
                    moveAgent.traceTarget = playerTr.position;
                    animator.SetBool(hashMove, true);
                    break;

                case State.ATTACK:
                    moveAgent.Stop();
                    animator.SetBool(hashMove, false);
                    break;

                case State.DIE:
                    moveAgent.Stop();
                    break;
            }
        }
    }




    IEnumerator CheckState(){
        while(!isDie){
            if(state == State.DIE) yield break;

            float dist = Vector3.Distance(playerTr.position, enemyTr.position);

            if(dist <= attackDist){
                state = State.ATTACK;
            }
            else if (dist <= traceDist){
                state = State.TRACE;
            }
            else{
                state = State.PATROL;
            }
            yield return ws;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(hashSpeed, moveAgent.speed);
    }
}
