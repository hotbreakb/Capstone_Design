using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGlitchEffect : MonoBehaviour
{
    private Animation anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }
    public void Play()
    {
        anim.Play();
    }
}
