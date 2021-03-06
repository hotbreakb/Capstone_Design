using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveAgent : MonoBehaviour
{
    //순찰 지점들을 저장하기 위한 List 타입 변수
    public List<Transform> wayPoints;

    //다음 순찰 지점의 배열 Index
    public int nextIdx = 0;

    //NavMeshAgent 컴포넌트를 저장할 변수
    private NavMeshAgent agent;


    private readonly float patrollSpeed = 1.5f;
    private readonly float traceSpeed = 4.0f;

    // 순찰 여부를 판단하는 변수
    private bool _patrolling;

    public bool patrolling{
        get{return _patrolling; }
        set{
            _patrolling = value;

            if(_patrolling){
                agent.speed = patrollSpeed;
                MoveWayPoint();
            }
        }
    }

    // 추적 대상의 위치를 저장하는 변수

    private Vector3 _traceTarget;

    public Vector3 traceTarget{
        get {return _traceTarget;}
        set{
            _traceTarget = value;
            agent.speed = traceSpeed;
            TraceTarget(_traceTarget);
        }
    }


    public float speed{
        get{return agent.velocity.magnitude;}
        
    }


    void TraceTarget(Vector3 pos){
        if(agent.isPathStale) return;

        agent.destination = pos;
        agent.isStopped = false;


    }






    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.speed = patrollSpeed;
        var group = GameObject.Find("WayPointGroup");
        if(group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);
        }
        MoveWayPoint();
    }


    private void MoveWayPoint()
    {
        //최단거리 경로 계산이 끝나지 않았으면 다음을 수행하지 않음
        if (agent.isPathStale) return;

        //다음 목적지를 wayPoints 배열에서 추출한 위치로 지정
        agent.destination = wayPoints[nextIdx].position;
        agent.isStopped = false;
    }


    public void Stop(){
        agent.isStopped = true;
        // 바로 정지하기 위해 속도를 0으로 설정
        agent.velocity = Vector3.zero;
        _patrolling = false;

    }








    // Update is called once per frame
    void Update()
    {   
        if(!_patrolling) return;



        //NavMeshAgent가 이동하고 있고 목적지에 도착했는지 여부를 계산
        if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {
            //다음 목적지의 배열 첨자를 계산
            nextIdx = ++nextIdx % wayPoints.Count;
            //다음 목적지로 이동 명령을 수행
            MoveWayPoint();
        }
    }
}
