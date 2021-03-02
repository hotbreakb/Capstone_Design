using UnityEngine;
using UnityEngine.SceneManagement;
using Leap;
using UnityEngine.UI;

public class CheckLeapmotion : MonoBehaviour
{
    Controller leapmotion;
    public Text notConnected;

    private void Start()
    {
        leapmotion = new Controller();

        if (!leapmotion.IsConnected)
        {
            leapmotion = new Controller(1);
        }

        notConnected.gameObject.SetActive(false);
    }

    public void onClickLikeButton()
    {
        if (leapmotion.IsConnected)
            Invoke("LoadNextScene", 2.0f);
        else
        {
            notConnected.gameObject.SetActive(true);
        }
    }

    private void LoadNextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }
}