using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosInfo : MonoBehaviour
{
    public static bool[] visited = new bool[4];


    // 접어서 인스펙터창으로 표시
    public  Transform pos1;
    public  Transform pos2;
    public  Transform pos3;
    public  Transform pos4;

    public  static Vector3[] shootingPos = new Vector3[4];         // 적군이 걸어서 해당위치에 도착하는 위치정보

    public static int maxEnmey;
    public static bool[] test = new bool[4];



    void Awake(){
        shootingPos[0] = pos1.position;
        shootingPos[1] = pos2.position;
        shootingPos[2] = pos3.position;
        shootingPos[3] = pos4.position;
        maxEnmey = 0;
    }
}
