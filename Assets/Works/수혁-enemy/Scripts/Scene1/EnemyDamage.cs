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

    private void OnTriggerEnter(Collider other){
        if(other.tag=="Bullet"){
        curHp-=25;
        if(curHp ==0){
            Destroy(gameObject);
        }
        hpbar.rectTransform.localScale = new Vector3(((float)curHp/(float)maxHp),1f,1f);
        }
    }
}
