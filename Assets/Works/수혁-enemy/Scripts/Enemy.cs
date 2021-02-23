using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    [SerializeField]
   private Animator animator;

    private const string bulletTag = "Bullet";

    [SerializeField] GameObject bulletPrefab;   // 총알 프리팹 (현재 총알에셋 없이 TmpBullet으로 대체)
    [SerializeField] GameObject bulletSpawn;     // 총구( 총알의 발사 위치)
    [SerializeField] GameObject player;

    [Range(0.1f, 1.0f)]
    public float attackProbability = 0.5f; // 공격가능성

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(player);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);


        float random = Random.Range(0.0f, 0.4f);


        if (random > attackProbability)
        {
            Shoot();
        }

        //Shoot();        // 고민해봐야 할 게 적이 공격하는 조건에 대해서 생각하기
    }

    void Shoot()
    {

        Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == bulletTag)
        {
            Debug.Log("적중");
        }
    }

}