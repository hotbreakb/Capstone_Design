using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
   public GameObject Enemy;
    
    public Transform Enemyspawn1;
    
    public Transform Enemyspawn2;
    public Transform Enemyspawn3;

    //public GameObject spawnPos;
    public GameObject[] spawnPos;


    void Awake(){
        spawnPos = GameObject.FindGameObjectsWithTag("spawn");
    }




    void SpawnEnemy(){
        int ran = Random.Range(0,spawnPos.Length);
        Vector3 pos = spawnPos[ran].transform.position;
        Quaternion quaternion = spawnPos[ran].transform.rotation; 
        GameObject enemy = (GameObject)Instantiate(Enemy,pos,quaternion);
    }

  
    void Update(){
        
        if(PosInfo.maxEnmey < PosInfo.visited.Length){
            PosInfo.maxEnmey+=1;
            Debug.Log(PosInfo.maxEnmey+" "+PosInfo.visited.Length);
            SpawnEnemy();
        }

    }
}
