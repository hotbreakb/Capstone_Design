using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
   public GameObject Enemy;
    
    public Transform Enemyspawn1;
    
    public Transform Enemyspawn2;
    public Transform Enemyspawn3;
    public Transform Enemyspawn4;

    //public GameObject spawnPos;
    public GameObject[] spawnPos;


    void Awake(){
        spawnPos = GameObject.FindGameObjectsWithTag("spawn");
    }




    void SpawnEnemy(){
        int ran = Random.Range(0,spawnPos.Length+1);
        Debug.Log(ran +" "+spawnPos.Length);
        Vector3 pos = spawnPos[ran].transform.position;
        Quaternion quaternion = spawnPos[ran].transform.rotation; 
        GameObject enemy = (GameObject)Instantiate(Enemy,pos,quaternion);
    }

    int cnt = 0;        // 확인용 나주에 지울거
    void Update(){
        cnt++;          // 일단 4마리만 추가 나중에 시간대별로 나오게 하기
        if(cnt <=10){
            SpawnEnemy();
        }
    }
}
