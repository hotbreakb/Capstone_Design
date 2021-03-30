using UnityEngine.UI;
using UnityEngine;

public class Locked : MonoBehaviour
{
    private Animator anim;
    private Image img;

    void Start()
    {
        if (FindObjectOfType<GameManager>().isPlayerWin) FadeOut();
        else anim.Play("LockedAnim");
    }

    void FadeOut()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
        }
    }

}
