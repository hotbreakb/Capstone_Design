using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakWindow : MonoBehaviour
{
    private Animator anim;
    public bool isBroken = false; /* Used in HandgunScript */
    public int maxShootCount = 3;
    private int shootCount = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBroken)
        {
            shootCount++;

            if(shootCount == maxShootCount && anim != null)
            {
                anim.Play("Break", 0, 0f);
                isBroken = true;
            }
        }
    }
}
