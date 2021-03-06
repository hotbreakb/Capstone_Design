using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using System.Linq;

public class LeapmotionGesture : MonoBehaviour
{
    private List<Finger> fingers;
    public GameObject cube;

    /* AutomaticGunScriptLPFP.cs에서 사용하기 위해 public으로 선언 */
    public bool isShoot = false;   // 총쏘기
    public bool isGrenade = false; // 수류탄
    public bool isLoading = false; // 장전

    void Start()
    {
        cube = GameObject.FindGameObjectWithTag("Cube");
    }

    void Update()
    {
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
            Invoke("QuitGame", 10.0f); // 10초 이내 연결되지 않으면 스크립트 종료
            Debug.Log("not connected");
        }
        else
        {
            CancelInvoke("QuitGame");
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

            StartCoroutine("timer");

            // [Conditions to 'Shoot']
            //  1. Two straight fingers
            //  2. Hands moving from top to bottom

            Debug.Log(System.Math.Abs(handPalmPosition.x - prehandPalmPosition.x));

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

            // isShoot = false; isGrenade = false; isLoading = false;

            //Debug.Log("isShoot: " + isShoot + " isGrenade" + isGrenade + "isLoading" + isLoading);
            yield return new WaitForSeconds(1.0f);
            //yield return null;
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

        //Debug.Log(extendedFingers);
        return extendedFingers;
    }

    IEnumerator timer()
    {
        int time = 0;
        while (true)
        {
            time++;
            yield return new WaitForSeconds(5.0f); // delay 2 seconds
        }
    }
}