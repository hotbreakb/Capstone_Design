using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{   
    private AudioSource audio;
    private Animator animator;

    private Transform playerTr;

    private Transform enemyTr;

    private readonly int hashFire = Animator.StringToHash("Fire");

    private float nextFire = 0.0f;
    private readonly float fireRate = 0.1f;
    private readonly float damping = 10.0f;

    public bool isFire = false;
    public AudioClip fireSfx;

    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFire){
            if(Time.time >= nextFire){
                Fire();
                nextFire = Time.time + fireRate + Random.Range(0.0f,0.3f);
            }
        }


        Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
        enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        

    }

    private void Fire(){
        animator.SetTrigger(hashFire);
        audio.PlayOneShot(fireSfx, 1.0f);
    }
}
