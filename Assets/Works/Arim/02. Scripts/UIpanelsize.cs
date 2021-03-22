using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIpanelsize : MonoBehaviour
{
    public GameObject infoUI;
    public Image infoPanel;

    public GameObject text;
    private float totaltime = 0f;
    private float fills;
    private float timespeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
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
        yield return new WaitForSeconds(1.5f);
        text.SetActive(true);
    }

    IEnumerator DestroyUI()
    {
        yield return new WaitForSeconds(2f);
        infoUI.SetActive(false);

    }

}
