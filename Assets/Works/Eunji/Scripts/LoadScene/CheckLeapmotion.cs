using UnityEngine;
using TMPro;

public class CheckLeapmotion : MonoBehaviour
{
    public TextMeshProUGUI notConnected;
    public UnityEngine.UI.Image circle;

    private void Start()
    {
        notConnected.gameObject.SetActive(false);
        // circle.gameObject.SetActive(false);

        if (FindObjectOfType<GameManager>().isLeapMotionConnected)
            GetComponent<LoadSceneAuto>().GameStart();
        else
        {
            notConnected.gameObject.SetActive(true);
            circle.gameObject.SetActive(true);
        }
    }
}