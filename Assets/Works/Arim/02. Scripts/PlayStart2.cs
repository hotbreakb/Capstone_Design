using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStart2 : MonoBehaviour
{
    public float changeTime = 20.0f;
    public GameObject Space;
    //public Transform Tspace;
    public List<Light> lights;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ChangeScale()
    {
        Space.transform.localScale = new Vector3(10, 10, 10);
        var group = GameObject.Find("PointLights");
        if (group != null)
        {
            group.GetComponentsInChildren<Light>(lights);
            lights.range
        }
    }
}
