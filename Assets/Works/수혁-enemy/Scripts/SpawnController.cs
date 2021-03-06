// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SpawnController : MonoBehaviour
// {

//     [SerializeField] float spawnInterval = 3f;      // 죽었전 적이 새로 리스폰 되는 간격으로 인스펙터 창에서 조절가능하게 냅뒀습니다.

//     //EnemySpawner[] spawners;    // 적군들이 리스폰 될 장소를 담을 배열

//     float timer = 0f;       // 출현시간 판정용의 타이머 변수


//     void Start()
//     {
//         //spawners = GetComponentsInChildren<EnemySpawner>();    
//     }

//     void Update()
//     {
//         // 타이머 갱신
//         timer += Time.deltaTime;
        
//         // 출현 간격의 판정
//         if(spawnInterval < timer)
//         {
//             // 랜덤으로 EnemySpanwer를 선택해서 적을 출현시킨다.
//             var index = Random.Range(0, spawners.Length);
//             //spawners[index].Spawn();

//             timer = 0f;
//         }
//     }
// }
