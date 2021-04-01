using UnityEngine;
using UnityEngine.UI;


public class SwipeMenu : MonoBehaviour
{
    public GameObject scrollbar;
    private float scroll_pos;
    private float distance;
    private float[] pos;

    private int levelSize = 6;
    private bool _isClicked = false;
    void Start()
    {
        pos = new float[levelSize];
        distance = 1f / (pos.Length - 1f);

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }

    void Update()
    {
        if (_isClicked)
        {
            // scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[levelSize - 1], 0.01f);
            Invoke("TurnOff", 2f);
        }
        else
        {
            // for (int i = 0; i < pos.Length; i++)
            // {
                // if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                // {
                    
                // }
            // }
        }
        
    }

    public void IsClicked()
    {
        if (_isClicked == false) _isClicked = true;
    }

    private void TurnOff() {
        _isClicked = false;
    }
}
