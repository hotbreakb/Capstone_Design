using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakWindow : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(anim != null)
        {
            Debug.Log("ontrigger");
            anim.Play("Break");
        }
    }
}
