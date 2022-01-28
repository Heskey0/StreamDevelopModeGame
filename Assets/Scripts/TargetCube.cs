using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;

public class TargetCube : MonoBehaviour
{
    public float Speed = 3f;
    private GameObject particle;
    private string ParticlePath = "Particle System";

    private float curFL = 0;
    private float maxFL = 1.5f;
    public bool isVertical=false;
    private bool againstingFloor=false;

    void Start()
    {
        particle = Resources.Load<GameObject>("" + ParticlePath);
        this.OnTriggerEnter2DAsObservable()
            .Where(c => c.CompareTag("Player"))
            .Subscribe(c =>
            {
                Debug.Log("kill");
                particle.transform.position = transform.position;
                GameObject.Instantiate(particle);
                KillSelf();
            });

        this.OnTriggerEnter2DAsObservable()
            .Where(c => c.CompareTag("Floor"))
            .Subscribe(c =>
            {
                againstingFloor = true;
                //curFL += Time.deltaTime;
            });
        this.OnTriggerExit2DAsObservable()
            .Where(c => c.CompareTag("Floor"))
            .Subscribe(c =>
            {
                againstingFloor = false;
                //curFL += Time.deltaTime;
            });
        
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                curFL += Time.deltaTime;
                if (curFL >= maxFL)
                {
                    curFL = 0;
                    Speed = -Speed;
                }
                
                if (againstingFloor)
                {
                    Speed = -Speed;
                }
                
                if (isVertical)
                {
                    transform.Translate(0, Speed*Time.deltaTime, 0);
                }
                else
                {
                    transform.Translate(Speed*Time.deltaTime, 0, 0);
                }
       
                
            });
    }

    void KillSelf()
    {
        GameObject.Destroy(gameObject);
    }
}