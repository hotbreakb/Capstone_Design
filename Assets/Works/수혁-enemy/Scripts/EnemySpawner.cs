using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] Enemy enemyPrefab; // 출현시키는 적의 프리팹

    Enemy enemy;    // 출현 적인 적을 보유


    public void Spawn()
    {
        if(enemy == null)
        {
            enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
