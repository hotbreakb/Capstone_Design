using UnityEngine;

public class LockClicked : MonoBehaviour
{
    public void click(){
        GetComponentInChildren<PlayAnimInLock>().playAnim();
    }
}
