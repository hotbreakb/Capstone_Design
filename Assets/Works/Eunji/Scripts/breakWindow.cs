using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakWindow : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private bool isBroken = false;
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
                Debug.Log("ontrigger");
                anim.Play("Break", 0, 0f);
                isBroken = true;
            }
        }
        

        Debug.Log(shootCount);
    }
}
