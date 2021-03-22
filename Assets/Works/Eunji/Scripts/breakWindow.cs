using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakWindow : MonoBehaviour
{
    private string SceneName;

    private Animator anim;
    private AudioSource breakAudioSource;

    public bool isBroken = false; /* Used in HandgunScript */
    public int maxShootCount = 3;
    private int shootCount = 0;

    private void Start()
    {
        SceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        anim = GetComponent<Animator>();
        breakAudioSource = GetComponent<AudioSource>();
    }

    // In Playmode
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

    // In Playmode2
    private void OnCollisionEnter(Collision collision)
    {
        if (!isBroken)
        {
            shootCount++;

            if (shootCount == maxShootCount && anim != null)
            {
                anim.Play("Break", 0, 0f);
                breakAudioSource.Play();

                isBroken = true;
            }
        }
    }
}
