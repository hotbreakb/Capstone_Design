using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{

    // 적 이동 관련된 것////////////////////////
    private Transform _transform;
    private Transform destinationTransform;    // 목적지 위치
    private NavMeshAgent navMeshAgent;

    ///////////////////////////////////////////////
   
    Animator animator;

    private const string bulletTag = "Bullet";

    [SerializeField] GameObject bulletPrefab;   // 총알 프리팹 (현재 총알에셋 없이 TmpBullet으로 대체)
    [SerializeField] GameObject bulletSpawn;     // 총구( 총알의 발사 위치)
    [SerializeField] GameObject player;


    [Range(0.1f, 1.0f)]
    public float attackProbability = 0.5f; // 공격가능성


    NavMeshAgent agent;



    bool flag = false;      // 기본 로직은 적이 정해진 장소로 이동하고 총을 쏘는 것이니 
                            // 정해진 장소로 이동했을때만 총을 쏘도록 관리하는 bool

    private void Awake(){
       
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start(){
        for(int i=0; i<PosInfo.visited.Length; i++){
            if(!PosInfo.visited[i]){
                //transform.LookAt(PosInfo.shootingPos[i]);
                PosInfo.visited[i] = true;
                animator.SetBool("isRun",true);
                agent.SetDestination(PosInfo.shootingPos[i]);
                break;
              }
        }
    }





    // Update is called once per frame
    void Update()
    {   
        if(!agent.pathPending){     // 목적지 도착하는지 여부
            if(agent.remainingDistance<= agent.stoppingDistance){
                animator.SetBool("isRun",false);
                flag = true;
            }
        }



        // if(flag){           // 목적지 도착하고 놔서 총을 쏴야하니 flag로 관리
        //     transform.LookAt(player.transform.position);
        //     float random = Random.Range(0.0f, 0.4f);

        //     if (random > attackProbability)
        //     {   
        //         animator.SetBool("isShoot",true);
        //         Shoot();
        //     }
        //     else{
        //         animator.SetBool("isShoot",false);
        //     }
        // }
    

            transform.LookAt(player.transform.position);
            float random = Random.Range(0.0f, 0.4f);

            if (random > attackProbability)
            {   
                animator.SetBool("isShoot",true);
                Shoot();
            }
            else{
                animator.SetBool("isShoot",false);
            }
    }

    void Shoot()
    {

        Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
    }

    private void OnTriggerEnter(Collider coll){
        if(coll.tag == bulletTag){
            this.SendMessage("UpdateAfterReceiveAttack");
            Debug.Log("제발...");
        }
    }
    
 

}