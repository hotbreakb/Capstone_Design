using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    
   
    Animator animator;

    private const string bulletTag = "Bullet";

    [SerializeField] GameObject bulletPrefab;   // 총알 프리팹 (현재 총알에셋 없이 TmpBullet으로 대체)
    [SerializeField] GameObject bulletSpawn;     // 총구( 총알의 발사 위치)
    [SerializeField] GameObject player;

    [Range(0.1f, 1.0f)]
    public float attackProbability = 0.5f; // 공격가능성

    private void Awake(){
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);


        float random = Random.Range(0.0f, 0.4f);


        if (random > attackProbability)
        {   
            Debug.Log(animator);
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

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == bulletTag)
        {
            Debug.Log("적중");
        }
    }

}