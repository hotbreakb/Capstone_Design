using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // 적 캐릭터가 출현할 위치를 담을 배열
    public Transform[] points;
    public GameObject enemy;
    // 적 캐릭터를 생성할 주기
    public float createTime = 2.0f;
    // 적 캐릭터의 최대 생성 개수
    public int maxEnemy = 10;
    // 게임종료 여부
    public bool isGameOver = false;

    public static GameManager instance = null;


    void Awake(){
        if(instance == null){
            instance = this;
        }
        else if(instance != this){
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }



    // Start is called before the first frame update

    void Start()
    {
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();    // 위치정보 갖고옴

        if(points.Length > 0){
            StartCoroutine(this.CreateEnemy());
        }
    }

    IEnumerator CreateEnemy(){
        while(!isGameOver){
            int enemyCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;

            if(enemyCount < maxEnemy){
                yield return new WaitForSeconds(createTime);

                int idx = Random.Range(1,points.Length);
                Instantiate(enemy, points[idx].position, points[idx].rotation);
            }
            else{
                yield return null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
