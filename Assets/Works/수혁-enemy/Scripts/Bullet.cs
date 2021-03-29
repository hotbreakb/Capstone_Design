using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1f; // 총알 속도
    [SerializeField] GameObject player;      // 나중에 플레이어 나오면 이거로 바꾸기


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        
    
    }


     void OnTriggerEnter(Collider other)
    {
        // other.SendMessage("OnHitBullet");       // 아림님 플레이어 코드 받고 그쪽에서도 OnHitBullet 메소드 만들기
        Destroy(gameObject);
    }
}
