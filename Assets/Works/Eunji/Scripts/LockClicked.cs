using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockClicked : MonoBehaviour
{
    public void click(){
        GetComponentInChildren<Locked>().playAnim();
    }
}
