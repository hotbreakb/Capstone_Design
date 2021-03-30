using Kino;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    public TextMeshProUGUI YouWin;
    public TextMeshProUGUI GameOver;


    void Awake(){
        if(instance == null){
            instance = this;
        }
        else if(instance != this){
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();    // 위치정보 갖고옴

        if(points.Length > 0){
            StartCoroutine(this.CreateEnemy());
        }
    }

    IEnumerator CreateEnemy(){
        while(!isGameOver){
            int enemyCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;

            if(enemyCount < maxEnemy){
                yield return new WaitForSeconds(createTime);

                int idx = Random.Range(1,points.Length);
                Instantiate(enemy, points[idx].position, points[idx].rotation);
            }
            else{
                yield return null;
            }
        }
    }

    /* ------------------------------------------------------------------------------- */

    public void playerWin() {
        /* You Win UI 띄우기 */
        YouWin.gameObject.SetActive(true);
        GameObject.Find("Main Camera").GetComponent<PlayGlitchEffect>().Play();
        
        // 좌물쇠 풀리는 모양
        // Start
        Debug.Log("player win");
        Invoke("QuitGame", 2f);
    }

    public void playerLose() {
        /* Game over UI 띄우기 */
        GameOver.gameObject.SetActive(true);
        GameObject.Find("Main Camera").GetComponent<PlayGlitchEffect>().Play();
        
        // 자물쇠 모양으로 가서 안 풀린 거 보여주기
        // restart
        Debug.Log("player lose");
        Invoke("QuitGame", 2f);
    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
