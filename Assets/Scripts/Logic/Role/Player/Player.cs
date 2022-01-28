using System;
using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Player : Role
{
    private float curFL = 0;
    private float maxFL = 1;
    public float Speed = 10;

    private bool isSprinting = false;
    private Vector3 lastPos;

    private void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ =>
            {
                lastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            });

        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Subscribe(_ =>
            {
                if (isSprinting)
                {
                    return;
                }

                var dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastPos).normalized;
                dir.z = 0;
                StartCoroutine(Sprint(dir));
                Debug.Log(dir);
            });
    }

    IEnumerator Sprint(Vector3 dir)
    {
        isSprinting = true;
        while (true)
        {
            curFL += Time.deltaTime;
            if (curFL >= maxFL)
            {
                curFL = 0;
                break;
            }
            else
            {
                transform.position += dir * Time.deltaTime * Speed;
                yield return null;
            }
        }

        isSprinting = false;
        yield return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log(Shoot.bulletAmount);
        }
    }
}