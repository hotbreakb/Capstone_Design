using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Restart : MonoBehaviour
{
    public GameObject GrayPanel;
    public TextMeshProUGUI RestartText;
    public GameObject RestartBtn;
    public GameObject YesBtn;
    public GameObject NoBtn;

    public void playShow(bool _isStart){
        StartCoroutine(show(_isStart));
    }

    public IEnumerator show(bool _isStart) {
        if(_isStart) RestartText.text = "start?";

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
