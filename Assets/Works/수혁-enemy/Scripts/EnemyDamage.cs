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
        if(curHp ==0){

            // 지금 이 방식 문제가 적이 목적지로 가다가 중간에 죽으면
            // 몇번째 슈팅위치에서 죽는지 파악이 안돼서 스폰이 안된다.
            for(int i=0; i<PosInfo.visited.Length; i++){
                if(Vector3.Distance(PosInfo.shootingPos[i], gameObject.transform.position)<=0.5f){
                    PosInfo.visited[i] = false;
                    PosInfo.maxEnmey -=1;
                }
            }
            Destroy(gameObject);
        }
        hpbar.rectTransform.localScale = new Vector3((float)curHp/(float)maxHp,1f,1f);
    }
}
