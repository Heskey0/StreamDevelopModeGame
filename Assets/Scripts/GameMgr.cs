using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;
using UnityEngine.SocialPlatforms;

public class GameMgr : MonoBehaviour
{
    public Unity.Mathematics.Random r=new Unity.Mathematics.Random(111);
    public GameObject TargetCube;
    private float curFL=0;
    private float maxFL=2;
    void Start()
    {
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                curFL += Time.deltaTime;
                if (curFL>=maxFL)
                {
                    curFL = 0;
                    TargetCube.transform.position=new Vector3(Random.Range(0,25),Random.Range(-3,10),0);
                    TargetCube.GetComponent<TargetCube>().isVertical = r.NextBool();
                    GameObject.Instantiate(TargetCube);
                }
                
            });
    }

 
}
