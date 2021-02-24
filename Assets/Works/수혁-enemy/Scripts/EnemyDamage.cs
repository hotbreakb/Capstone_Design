using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyDamage : MonoBehaviour
{   

    public int maxHp =50;
     int curHp = 50;           // 현재 cpfur


    public Image hpbar;

    void awake(){
        hpbar.rectTransform.localScale = new Vector3(1f,1f,1f);
    }

    
    public void UpdateAfterReceiveAttack(){
        curHp-=25;
        Debug.Log("curHP" + curHp); 
        // if(curHp ==0){
        //     Destroy(gameObject);
        // }
        hpbar.rectTransform.localScale = new Vector3((float)curHp/(float)maxHp,1f,1f);
    }
}
