using Kino;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    // 적 캐릭터가 출현할 위치를 담을 배열
    public Transform[] points;
    public GameObject enemy;
    // 적 캐릭터를 생성할 주기
    public float createTime = 2.0f;
    // 적 캐릭터의 최대 생성 개수
    public int maxEnemy = 10;
    // 게임종료 여부
    public bool isGameOver = false;

    /* ---------------------------------- */

    public TextMeshProUGUI YouWin;
    public TextMeshProUGUI GameOver;

    public bool isPlayerWin = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (!GameObject.Find("SpawnPoint")) return;

        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();    // 위치정보 갖고옴

        if (points.Length > 0)
        {
            StartCoroutine(this.CreateEnemy());
        }


    }

    IEnumerator CreateEnemy()
    {
        while (!isGameOver)
        {
            int enemyCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (enemyCount < maxEnemy)
            {
                yield return new WaitForSeconds(createTime);

                int idx = Random.Range(1, points.Length);
                Instantiate(enemy, points[idx].position, points[idx].rotation);
            }
            else
            {
                yield return null;
            }
        }
    }

    /* ------------------------------------------------------------------------------- */

    public void playerWin()
    {
        /* You Win UI 띄우기 */
        if (!YouWin) return;

        YouWin.gameObject.SetActive(true);
        GameObject.Find("Main Camera").GetComponent<PlayGlitchEffect>().Play();


        // 좌물쇠 풀리는 모양
        // Start
        Debug.Log("player win");
        StartCoroutine(ShowLevelTimer());

        GameObject.Find("Canvas").GetComponent<Locked>()._isPlayerWin = true;
        isPlayerWin = true;
    }

    public void playerLose()
    {
        /* Game over UI 띄우기 */
        if (!GameOver) return;

        GameOver.gameObject.SetActive(true);
        GameObject.Find("Main Camera").GetComponent<PlayGlitchEffect>().Play();

        // restart
        Debug.Log("player lose");
        StartCoroutine(ShowLevelTimer());

        GameObject.Find("Canvas").GetComponent<Locked>()._isPlayerWin = false; /* using in UI Locked */
        isPlayerWin = false;
    }

    IEnumerator ShowLevelTimer()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        SceneManager.LoadScene("Level");
    }
}
