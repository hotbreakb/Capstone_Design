using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // 스파크 프리랩
    public GameObject sparkEffect;

<<<<<<< Updated upstream
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Bullet")
        {
            //ShowEffect(coll);
            Destroy(coll.gameObject);
=======
    //private void OnTriggerEnter(Collision coll)
    //{
    //    if (coll.collider.tag == "Bullet")
    //    {
    //        ShowEffect(coll);
    //        Destroy(coll.gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            //ShowEffect(other);
            Destroy(other.gameObject);
>>>>>>> Stashed changes
        }
    }

    void ShowEffect(Collision coll)
    {
        // 충돌 지점의 정보를 추출
        ContactPoint contact = coll.contacts[0];
        // 법선 벡터가 이루는 회전각도를 추출
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);

        // 스파크 효과를 생성
        Instantiate(sparkEffect, contact.point, rot);
    }
}
