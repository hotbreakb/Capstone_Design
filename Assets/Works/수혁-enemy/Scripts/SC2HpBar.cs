using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SC2HpBar : MonoBehaviour
{

     float maxHp =100f;
     float curHp = 100f;           // 현재 cpfur


    public Image hpbar;

    void awake(){
        hpbar.rectTransform.localScale = new Vector3(1f,1f,1f);
    }

    
    public void UpdateAfterReceiveAttack(){
        curHp-=34.0f;
        if(curHp <=0){

            GameObject hp = gameObject.transform.Find("HPBarPos").gameObject;
            hp.SetActive(false);
 
        }
        hpbar.rectTransform.localScale = new Vector3((float)curHp/(float)maxHp,1f,1f);
    }
 
}
