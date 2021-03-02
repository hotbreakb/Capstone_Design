using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCount : MonoBehaviour
{
    public GameObject checkHandAction;
    public Text countDown;
    public int count;
    private bool isCountChanged = false;
    private Color yellow = Color.yellow;
    private Color Black = Color.black;

    // Start is called before the first frame update
    void Start()
    {
        countDown.text = count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountChanged == false && checkHandAction.GetComponent<Renderer>().material.color == yellow)
        {
            countDown.text = (count--).ToString();
            isCountChanged = true;
            StartCoroutine("timer");
        }
        else if(checkHandAction.GetComponent<Renderer>().material.color == Black)
            isCountChanged = true;
        else
            isCountChanged = false;
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(10.0f);
    }
}
