using UnityEngine;
using UnityEngine.UI;


public class SwipeMenu : MonoBehaviour
{
    public GameObject scrollbar;
    private float distance;
    private float posEnd;

    private int levelSize = 8;
    private bool _rightClicked = false;
    private bool _leftClicked = false;
    void Start()
    {
        distance = 1f / (levelSize - 1f);
        posEnd = distance * (levelSize - 1) + 0.023f;
    }

    void Update()
    {
        if (_rightClicked)
        {
            scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, posEnd, 0.03f);
            Invoke("TurnOff", 2f);
        }
        else if(_leftClicked){
            scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, 0, 0.03f);
            Invoke("TurnOff", 2f);
        }
    }

    public void RightClicked()
    {
        if (_rightClicked == false) _rightClicked = true;
    }

    public void LeftClicked(){
        if (_leftClicked == false) _leftClicked = true;
    }

    private void TurnOff() {
        _rightClicked = false;
        _leftClicked = false;
    }
}
