using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGlitchEffect : MonoBehaviour
{
    private Animator anim;

    public void Play()
    {
        anim.Play("GlitchEffect", 0, 0f);
    }
}
