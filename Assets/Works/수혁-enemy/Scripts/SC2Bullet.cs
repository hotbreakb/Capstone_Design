using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC2Bullet : MonoBehaviour
{
    [SerializeField] float speed = 100000000f; // 총알 속도
    private GameObject player;      // 나중에 플레이어 나오면 이거로 바꾸기

    void Awake(){
        player = GameObject.FindWithTag("spot");
    }
    void Start(){
        //GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
    }
    // Update is called once per frame
    void Update()
    {   

        player = GameObject.FindWithTag("spot");
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        
    }


     void OnTriggerEnter(Collider other)
    {
        // other.SendMessage("OnHitBullet");       // 아림님 플레이어 코드 받고 그쪽에서도 OnHitBullet 메소드 만들기
        Destroy(gameObject);
    }
}
