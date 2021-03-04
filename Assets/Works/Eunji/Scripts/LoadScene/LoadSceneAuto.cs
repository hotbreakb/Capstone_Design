using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAuto : MonoBehaviour
{
    // ---- Tutorial_01 ----
    // If the Arrow Button is clicked, 'Tutorial_02' is loaded.
    
    // Go to 'Tutorial_04'
    // Test Leapmotion Hand's Action
    // 1) Loading  2) Grenade  3) Shooting
    public void LoadNextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
        StartCoroutine("timer");
    }

    // Go to 'Tutorial_07'
    // Pass the Leapmotion Test
    public void LoadGameScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 4); 
        StartCoroutine("timer");
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(5.0f);
    }
}
