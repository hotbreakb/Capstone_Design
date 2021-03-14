using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakWindow : MonoBehaviour
{
    private Animator anim;
    private AudioSource breakAudioSource;

    public bool isBroken = false; /* Used in HandgunScript */
    public int maxShootCount = 3;
    private int shootCount = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        breakAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBroken)
        {
            shootCount++;

            if(shootCount == maxShootCount && anim != null)
            {
                anim.Play("Break", 0, 0f);
                breakAudioSource.Play();

                isBroken = true;
            }
        }
    }
}
