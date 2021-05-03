using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScene : MonoBehaviour
{   
    Animator ani;
    Transform playerTr;
    Transform enemyTr;
    private readonly float damping = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.Find("tt").GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        ani = GetComponent<Animator>();
        Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
        enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
    }

    // Update is called once per frame
    void Update()
    {
        // Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
        // Debug.Log("변경전 " +enemyTr.rotation);
        // enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        // Debug.Log("변경후 " +enemyTr.rotation);
        // //ani.SetLookAtPosition(enemyTr);
    }
}
