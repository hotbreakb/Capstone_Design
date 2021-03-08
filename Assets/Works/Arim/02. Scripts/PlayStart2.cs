using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStart2 : MonoBehaviour
{
    public float changeTime = 20.0f;
    public GameObject Player;
    public GameObject TmpPlayer;
    public GameObject Enemy;
    public GameObject bullet;
    public GameObject bulletCase;
    public GameObject desk;
    //public GameObject labtop;
    public GameObject grenade;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Changes();
    }
    void Changes()
    {
        Player.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        Enemy.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        TmpPlayer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        bullet.transform.localScale = new Vector3(0.004f, 0.004f, 0.004f);
        bulletCase.transform.localScale = new Vector3(0.11f, 0.11f, 0.11f);
        desk.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        desk.transform.position = new Vector3(desk.transform.position.x, -1.53f, desk.transform.position.z);  
        //labtop.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        grenade.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);


    }

}
