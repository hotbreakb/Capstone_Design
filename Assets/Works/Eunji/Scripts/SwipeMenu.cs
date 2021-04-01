using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public GameObject scrollbar;

    public GameObject ArrawBtn;
    private float scroll_pos;
    private float[] pos;

    void Update()
    {
        pos = new float[transform.childCount];
        float dis = 1f / (pos.Length - 1f);

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = dis * i;
        }

        if (Input.GetMouseButton(0))
        { // if arrow btn is clicked, the scroll is moved.
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (dis / 2) && scroll_pos > pos[i] - (dis / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }
    }
}
