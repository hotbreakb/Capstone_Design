using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagetest : MonoBehaviour
{
    private const string bulletTag = "BULLET";
    private GameObject bloodEffect;
    // Start is called before the first frame update
    void Start()
    {
        bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == bulletTag)
        {
            ShowBloodEffect(coll);
            Destroy(coll.gameObject);

        }
    }
    private void ShowBloodEffect(Collision coll)
    {
        Vector3 pos = coll.contacts[0].point;
        Vector3 _normal = coll.contacts[0].normal;
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);

        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot);
        Destroy(blood, 1.0f);
    }
}
