using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakWindow : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ontrigger");
        anim.Play("Break");
    }
}
