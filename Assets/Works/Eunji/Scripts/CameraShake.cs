using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeAmount;
    float ShakeTime;
    Vector3 initialPosition;

    public void VibrateForTime(float time)
    {
        ShakeTime = time;
        //Shake();
    }

    // Start is called before the first frame update
    void Start()
    {
        // MainCamera Position
        initialPosition = transform.position;
    }

    void Shake()
    {
        Debug.Log("shake time: " + ShakeTime);
        if (ShakeTime > 0)
        {
            Debug.Log("shake Camera");
            transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0f;
            transform.position = initialPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
