using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAuto : MonoBehaviour
{
    public void LoadNextScene()
    {
        StartCoroutine("timer");
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(5.0f);
    }
}
