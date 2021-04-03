using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Leap;
using System.Linq;

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

    /* ----------- Leap Motion ---------- */
    private List<Finger> fingers;
    public GameObject cube;
    public bool isShoot = false;   // 총쏘기
    public bool isGrenade = false; // 수류탄
    public bool isLoading = false; // 장전

    /* -----------Player Win/Lose ---------- */

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
        if (GameObject.FindGameObjectWithTag("Cube"))
            cube = GameObject.FindGameObjectWithTag("Cube");

        if (!GameObject.Find("SpawnPoint")) return;

        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();    // 위치정보 갖고옴

        if (points.Length > 0)
        {
            StartCoroutine(this.CreateEnemy());
        }
    }

    void Update()
    {
        if (cube || SceneManager.GetActiveScene().name == "Loading")
            StartCoroutine("checkHand");
    }

    IEnumerator checkHand()
    {
        Controller controller = new Controller(1);

        if (controller == null)
        {
            controller = new Controller(1);
            Debug.Log("controller is null");
        }

        if (!controller.IsConnected)
        {
            controller.StartConnection();
            Invoke("QuitGame", 20.0f); // 20초 이내 연결되지 않으면 스크립트 종료
            // Debug.Log("not connected");
        }
        else
        {
            CancelInvoke("QuitGame");

            if(SceneManager.GetActiveScene().name == "Loading")
                SceneManager.LoadScene("PlayMode"); // 수정하기
        }

        Hand hand = new Hand();
        Hand previous_hand = new Hand();

        Vector handPalmPosition = new Vector();
        Vector prehandPalmPosition = new Vector();

        Frame frame = controller.Frame();           // The latest frame
        Frame previous = controller.Frame(1);       // The previous frame

        for (int h = 0; h < frame.Hands.Count; h++)
        {
            hand = frame.Hands[0]; // 현재 나타나는 손
            handPalmPosition = hand.PalmPosition;   // 현재 손의 위치

            if (!previous.Hands.Any()) continue;
            previous_hand = previous.Hands[0]; // 이전 프레임에 나타나는 손
            prehandPalmPosition = previous_hand.PalmPosition;   // 이전 프레임의 손의 위치

            fingers = hand.Fingers; // 현재 손가락의 개수

            int _extendedFingers = getExtendedFingers();    // 함수를 호출하여 펼쳐진 손가락의 개수를 확인한다

            isShoot = false; isGrenade = false; isLoading = false;

            // [Conditions to 'Shoot']
            //  1. Two straight fingers
            //  2. Hands moving from top to bottom

            if (_extendedFingers == 2 && System.Math.Abs(handPalmPosition.y - prehandPalmPosition.y) > 5)
            {
                cube.GetComponent<MeshRenderer>().material.color = Color.red;
                isShoot = true;
            }
            // [Condition for changing weapons]
            //  1. Hands moving from side to side (swipe)
            else if (System.Math.Abs(handPalmPosition.x - prehandPalmPosition.x) > 10)
            {
                cube.GetComponent<MeshRenderer>().material.color = Color.green;
                isGrenade = true;
            }
            // [Condition to Load]
            //  1. Gripped left hand
            else if (hand.GrabStrength == 1 && _extendedFingers == 0)
            {
                cube.GetComponent<MeshRenderer>().material.color = Color.yellow;
                isLoading = true;
            }
            else
            {
                cube.GetComponent<MeshRenderer>().material.color = Color.black;
            }

            yield return null;
        } // end for
    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game

        //UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private int getExtendedFingers()
    {
        int extendedFingers = 0;

        for (int f = 0; f < fingers.Count; f++)
        {
            Finger digit = fingers[f];
            if (digit.IsExtended)
                extendedFingers++;
        }
        return extendedFingers;
    }

    /* ------------------------------------------------------------------------------- */

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
