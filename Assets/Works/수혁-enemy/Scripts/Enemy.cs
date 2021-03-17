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



    private SkinnedMeshRenderer skin ;
 


    [Range(0.1f, 1.0f)]
    public float attackProbability = 0.5f; // 공격가능성

    private readonly int hashOffset = Animator.StringToHash("offset");
    private readonly int hashWalkSpeed = Animator.StringToHash("walkSpeed");

    NavMeshAgent agent;

    // 이동 지점들을 저장히기 위한 List 타입 변수
    public List<Transform> wayPoints;

    private bool flag = false;

    private void Awake(){
        //skin = GameObject.Find("Soldier_mesh").GetComponent<SkinnedMeshRenderer>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        animator.SetFloat(hashOffset, Random.Range(0.0f,1.0f));
        animator.SetFloat(hashWalkSpeed, Random.Range(1.0f,1.2f));
    }

    void Start(){

        var group = GameObject.Find("SpawnPoint");

        if(group  !=null){
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);      // 최상위 폴더 제거 
        }

        MoveWayPoint();
    }



    private void MoveWayPoint(){ 
        // 최단 거리 계산이 끝나지 않았으면  return 
        if(agent.isPathStale) return;
        animator.SetBool("isRun",true);
        agent.destination = wayPoints[Random.Range(0,wayPoints.Count)].position;
       
    }

    // Update is called once per frame
    void Update()
    {       
        if(!agent.pathPending){     // 목적지 도착하는지 여부
            if(agent.remainingDistance<= agent.stoppingDistance){
                animator.SetBool("isRun",false);
                flag  = true;
            }
        }

            if(flag){
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
  
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
    }

    private void OnTriggerEnter(Collider coll){
        if(coll.tag == bulletTag){
            this.SendMessage("UpdateAfterReceiveAttack");
        }
    }
    
 

}