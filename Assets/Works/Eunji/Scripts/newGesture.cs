using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class newGesture : MonoBehaviour
{
    Controller controller;
    List<Finger> fingers;
    public GameObject cube;

    Hand hand;
    Hand previous_hand;

    Vector handPalmPosition;
    Vector prehandPalmPosition;


    void Start()
    {
        controller = new Controller();
        cube = GameObject.FindGameObjectWithTag("Cube");
    }

    void Update()
    {
        if (controller.IsConnected)
        {
            Frame frame = controller.Frame();           // The latest frame
            Frame previous = controller.Frame(1);       // The previous frame

            for (int h = 0; h < frame.Hands.Count; h++)
            {

                hand = frame.Hands[0];
                previous_hand = previous.Hands[0];

                handPalmPosition = hand.PalmPosition;
                prehandPalmPosition = previous_hand.PalmPosition;

                fingers = hand.Fingers;

                int _extendedFingers = getExtendedFingers();

                // [Conditions to 'Shoot']
                //  1. Two straight fingers
                //  2. Hands moving from top to bottom
                if (hand.IsRight && _extendedFingers == 2 && System.Math.Abs(handPalmPosition.y - prehandPalmPosition.y) > 5 && System.Math.Abs(hand.PalmVelocity.y) > 30)
                {
                    cube.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                // [Condition for changing weapons]
                //  1. Hands moving from side to side (swipe)
                else if (hand.IsRight && System.Math.Abs(handPalmPosition.x - prehandPalmPosition.x) > 5)
                {
                    cube.GetComponent<MeshRenderer>().material.color = Color.green;
                }
                // [Condition to Load]
                //  1. Gripped left hand
                else if (hand.IsLeft && hand.GrabStrength == 1)
                {
                    cube.GetComponent<MeshRenderer>().material.color = Color.yellow;
                }
                else
                {
                    cube.GetComponent<MeshRenderer>().material.color = Color.black;
                }
            }
        }
        else
        {
            Debug.Log("not connected");
        }
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

        Debug.Log(extendedFingers);
        return extendedFingers;
    }

}