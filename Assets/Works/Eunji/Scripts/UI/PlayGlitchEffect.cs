using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGlitchEffect : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void Play()
    {
        anim.Play("Glitch");
    }
}
