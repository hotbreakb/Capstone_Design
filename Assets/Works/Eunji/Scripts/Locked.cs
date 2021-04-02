using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Locked : MonoBehaviour
{
    private Animator anim;
    private Image img;
    public bool _isPlayerWin = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        img = GetComponent<Image>();
        
        if (_isPlayerWin) StartCoroutine(FadeOut());
        else anim.Play("LockAnim");
    }

    IEnumerator FadeOut()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

}
