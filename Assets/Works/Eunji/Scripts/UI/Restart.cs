using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public GameObject GrayPanel;
    public GameObject RestartBtn;
    public GameObject YesBtn;
    public GameObject NoBtn;

    public void playShow(){
        StartCoroutine(show());
    }

    public IEnumerator show() {
        yield return new WaitForSecondsRealtime(1.3f);
        GrayPanel.SetActive(true);
        RestartBtn.SetActive(true);
        YesBtn.SetActive(true);
        NoBtn.SetActive(true);
    }

    public void hide() {
        GrayPanel.SetActive(false);
        RestartBtn.SetActive(false);
        YesBtn.SetActive(false);
        NoBtn.SetActive(false);
    }
}
