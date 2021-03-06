using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStart2 : MonoBehaviour
{
    public float changeTime = 20.0f;
    public GameObject Space;
    public GameObject desk;
    public GameObject labtop;
    //public Transform Tspace;
    public Light light1;
    public Light light2;
    public Light light3;
    public Light light4;
    public Light light5;
    public Light light6;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Changes();
    }
    void Changes()
    {
        Space.transform.localScale = new Vector3(10, 10, 10);
        ChangeLight(light1);
        ChangeLight(light2);
        ChangeLight(light3);
        ChangeLight(light4);
        ChangeLight(light5);
        ChangeLight(light6);

    }
    void ChangeLight(Light light)
    {
        light.range = 90.0f;
        light.intensity = 7.0f;
    }
}
