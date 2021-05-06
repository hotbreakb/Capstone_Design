using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIfill : MonoBehaviour
{
    public Image infoPanel;

    public GameObject text;
    private float totaltime = 0f;
    private float fills;
    private float timespeed = 2.2f;
    // Start is called before the first frame update
    void Start()
    {
        //text.SetActive(false);
        StartCoroutine(UItext());
    }

    // Update is called once per frame
    void Update()
    {
        totaltime += Time.deltaTime;
        fills = totaltime * timespeed;
        infoPanel.fillAmount = fills;
    }
    IEnumerator UItext()
    {
        yield return new WaitForSeconds(0.5f);
        text.SetActive(true);
    }

    

}
