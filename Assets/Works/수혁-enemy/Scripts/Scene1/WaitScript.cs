using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitScript : MonoBehaviour
{
    private OneMoveAgent oneMoveAgent;
    private  OneEneyAI oneEneyAI;
    private EnemyDamage enemy;

    void Awake(){
       oneMoveAgent = GetComponent<OneMoveAgent>();
       oneEneyAI = GetComponent<OneEneyAI>();
       enemy = GetComponent<EnemyDamage>();

        oneMoveAgent.enabled = false;
        oneEneyAI.enabled = false;
        enemy.enabled = false;
    }

    void go(){
        
        oneMoveAgent.enabled = true;
        oneEneyAI.enabled = true;
        enemy.enabled = true;
    }
}
