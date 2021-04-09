using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Restart : MonoBehaviour
{
    public GameObject GrayPanel;
    public TextMeshProUGUI RestartText;
    public GameObject YesBtn;
    public GameObject YesBtn2;
    public GameObject NoBtn;

    public void playShow(){
        if(!GrayPanel || !RestartText || !YesBtn || !NoBtn) return;

        GrayPanel.SetActive(true);
        RestartText.gameObject.SetActive(true);
        YesBtn.SetActive(true);
        YesBtn2.SetActive(false);
        NoBtn.SetActive(true);
        // StartCoroutine(show(_isStart));
    }

    // public IEnumerator show(bool _isStart) {
    //     yield return null;
    // }

    public void hide() {
        if(!GrayPanel || !RestartText || !YesBtn || !NoBtn) return;

        GrayPanel.SetActive(false);
        RestartText.gameObject.SetActive(false);
        YesBtn.SetActive(false);
        NoBtn.SetActive(false);
    }
}
