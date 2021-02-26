using UnityEngine;
using UnityEngine.SceneManagement;
using Leap;


public class CheckLeapmotion : MonoBehaviour
{
    Controller leapmotion;

    private void Start()
    {
        leapmotion = new Controller();

        if (!leapmotion.IsConnected)
        {
            leapmotion = new Controller(1);
        }
    }

    public void onClickLikeButton()
    {
        if (leapmotion.IsConnected)
            Invoke("LoadNextScene", 2.0f);
    }

    private void LoadNextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }
}