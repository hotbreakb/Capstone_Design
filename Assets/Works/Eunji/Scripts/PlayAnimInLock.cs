using UnityEngine;

public class PlayAnimInLock : MonoBehaviour
{
    public Animator anim;

    public void playAnim(){
        anim.Play("LockAnim");
    }
}
