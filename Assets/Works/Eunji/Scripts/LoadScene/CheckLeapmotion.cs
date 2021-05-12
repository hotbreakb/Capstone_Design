using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class CheckLeapmotion : MonoBehaviour
{
    public Text connect_2;
    public TextMeshProUGUI number;
    public UnityEngine.UI.Image circle;

    float counter = 5.0f;


    void Update()
    {
        if (FindObjectOfType<GameManager>().isLeapMotionConnected)
        {
            connect_2.text = "립모션이 연결되었습니다!";

            if (SceneManager.GetActiveScene().name == "Loading")
                GetComponent<LoadSceneAuto>().GameStart();
            else if (SceneManager.GetActiveScene().name == "Loading2")
                GetComponent<LoadSceneAuto>().GameStart2();
            else // Tutorial_02
                GetComponent<LoadSceneAuto>().LoadNextScene();
        }

        else
        {
            circle.gameObject.SetActive(true);

            if (counter > 0)
            {
                counter -= Time.deltaTime;
                number.text = ((int)counter).ToString();
            }
            else
            {
                GetComponent<LoadSceneAuto>().QuitGame();
                // SceneManager.LoadScene("Menu");
            }
        }
    }
}