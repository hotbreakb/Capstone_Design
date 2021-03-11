using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlink : MonoBehaviour
{
    public RectTransform upperBox;
    public RectTransform lowerBox;
    public float speed = 0.70f;
    public int blinkTimes = 3;
    public float closeTime = 1.0f;
    public bool endClosing = false;


    private Vector3 originalUpperPosition;
    private Vector3 originalLowerPosition;
    private Vector3 endUpper;
    private Vector3 endLower;

    private int currentBlink = 1;

    public enum Action
    {
        Open,
        Close
    };

    void Awake()
    {
        originalUpperPosition = upperBox.position;
        originalLowerPosition = lowerBox.position;
    }

    void OnEnable()
    {
        
        StartCoroutine(blink());
    }

    private IEnumerator blink()
    {
        yield return new WaitForSeconds(closeTime);
        while (currentBlink <= blinkTimes)
        {
            endUpper = originalUpperPosition;
            endLower = originalLowerPosition;

            endUpper.y += (50 * currentBlink);
            endLower.y -= (50 * currentBlink);

            // open eyelids
            yield return moveEyelids(endUpper, endLower, Action.Open);

            // check if we want to end the blink closing the eyes
            if (currentBlink == blinkTimes && !endClosing)
            {
                originalUpperPosition.y = Screen.height * 2;
                originalLowerPosition.y = -Screen.height;
            }

            // close eyelids
            yield return moveEyelids(originalUpperPosition, originalLowerPosition, Action.Close);

            currentBlink++;
        }
    }

    private IEnumerator moveEyelids(Vector3 upperLid, Vector3 lowerLid, Action action)
    {
        float elapsedTime = 0;

        while (elapsedTime < speed)
        {
            float duration = (elapsedTime / speed);

            if (action == Action.Open)
            {
                upperBox.position = Vector3.Lerp(originalUpperPosition, upperLid, duration);
                lowerBox.position = Vector3.Lerp(originalLowerPosition, lowerLid, duration);
            }
            else
            {
                upperBox.position = Vector3.Lerp(endUpper, upperLid, duration);
                lowerBox.position = Vector3.Lerp(endLower, lowerLid, duration);
            }

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}