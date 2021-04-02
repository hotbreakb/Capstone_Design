using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneEnmeyFire : MonoBehaviour
{
    private Animator animator;

    private Transform playerTr;


    private readonly int hashFire = Animator.StringToHash("isFire");
   // private readonly int hashReload = Animator.StringToHash("Reload");

    private float nextFire = 0.1f;
    private readonly float fireRate = 0.3f;
    private readonly float damping = 10.0f;

    private readonly float reloadTime = 2.0f;
    private readonly int maxBullet = 10;
    private int currBullet = 10;
    private bool isReload = false;
    private WaitForSeconds wsReload;


    public bool isFire = false;
    public GameObject Bullet;

    public GameObject bulletSpawn;     // 총구( 총알의 발사 위치)
    //public MeshRenderer muzzleFlash;



    // Start is called before the first frame update
    void Start()
    {   
  
        playerTr = GameObject.Find("TmpPlayer").GetComponent<Transform>();  
        //muzzleFlash.enabled = false;
        

        animator = GetComponent<Animator>();
        wsReload = new WaitForSeconds(reloadTime);
    }

    void Update(){

        if(!isReload && isFire){
            if(Time.time >= nextFire){
                animator.SetBool("isShoot",true);   
                Fire();
                animator.SetBool("isShoot",false);
                nextFire = Time.time + fireRate + Random.Range(0.0f,1.0f);  // 공속딜레이
            }
        }
    }
    void Fire()
    {   
        //Instantiate(Bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
    }
}
