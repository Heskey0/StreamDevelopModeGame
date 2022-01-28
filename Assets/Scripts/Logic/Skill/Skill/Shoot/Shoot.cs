﻿using System.Collections;
using System.Diagnostics;
using MiscUtil.Threading;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = Unity.Mathematics.Random;

public class Shoot : SkillBase
{
    public override int SkillId => 0;
    public override float[] Cd { get; set; }
    public static int bulletAmount = 0;
    
    public int _amount = 20;

    
    public GameObject blueBullet;
    private float _cd0 = 1;
    public GameObject pinkBullet;
    private float _cd1 = 0.01f;
    public GameObject pinkBullet2;
    private float _cd2 = 0.1f;
    public GameObject snipeBullet;
    private float _cd3 = 6f;

    private float deadTime = 0.2f;

    public static Random r = new Random(123);

    public Shoot(SkillCaster caster) : base(caster)
    {
        blueBullet = Resources.Load<GameObject>("Prefabs/Bullet/BlueBullet");
        pinkBullet = Resources.Load<GameObject>("Prefabs/Bullet/PinkBullet");
        pinkBullet2 = Resources.Load<GameObject>("Prefabs/Bullet/PinkBullet2");
        snipeBullet = Resources.Load<GameObject>("Prefabs/Bullet/SnipeBullet");
        
        Cd = new float[] {0, 0, 0, 0};
    }

    public override float GetCd(int equipId)
    {
        switch (equipId)
        {
            case 0:
                return Cd[0] / _cd0;
                break;
            case 1:
                return Cd[1] / _cd1;
                break;
            case 2:
                return Cd[2] / _cd2;
                break;
            case 3:
                return Cd[3] / _cd3;
                break;
            default:
                return 0;
        }
    }

    public override void Cast(int equipId, int param)
    {
        _amount = param;
        switch (equipId)
        {
            case 0:
                if (Cd[0] <= 0)
                {
                    Cd[0] = _cd0;
                    QuickCoroutine.Instance.StartCoroutine(shoot(0));
                }

                break;
            case 1:
                if (Cd[1] <= 0)
                {
                    Cd[1] = _cd1;
                    QuickCoroutine.Instance.StartCoroutine(shoot(1));
                }

                break;
            case 2:
                if (Cd[2] <= 0)
                {
                    Cd[2] = _cd2;
                    QuickCoroutine.Instance.StartCoroutine(shoot(2));
                }

                break;
            case 3:
                if (Cd[3] <= 0)
                {
                    Cd[3] = _cd3;
                    Snipe();
                }
                break;
        }
    }

    private void Snipe()
    {
        snipeBullet.transform.position = Caster.owner.transform.position;
        GameObject.Instantiate(snipeBullet);
    }
    
    IEnumerator shoot(int equipId)
    {
        GameObject bullet;
        float3 p0;
        switch (equipId)
        {
            case 0:
                p0 = Caster.owner.transform.position;
                bullet = blueBullet;
                for (int i = 0; i < _amount; i++)
                {
                    float rot = r.NextFloat(-40, 40);
                    
                    bullet.transform.position = p0;
                    //blueBullet.transform.rotation
                    GameObject.Instantiate(bullet);
                }
                break;
            case 1:
                bullet = pinkBullet;
                for (int i = 0; i < _amount; i++)
                {
                    p0 = Caster.owner.transform.position;
                    float rot = r.NextFloat(-50, 50);
                    
                    bullet.transform.position = p0;
                    GameObject.Instantiate(bullet);
                    yield return null;
                }
                break;
            case 2:
                p0 = Caster.owner.transform.position;
                bullet = pinkBullet2;
                for (int i = 0; i < _amount; i++)
                {
                    float rot = r.NextFloat(-40, 40);
                    
                    bullet.transform.position = p0;
                    //
                    GameObject.Instantiate(bullet);
                }
                break;

            // case 0:
            //     _manager.Instantiate(_blueBulletE, bulletArr);
            //     float3 p0 = Caster.owner.transform.position;
            //     for (int i = 0; i < _amount; i++)
            //     {
            //         float rot = r.NextFloat(-40, 40);
            //         _manager.SetComponentData(bulletArr[i], new Translation {Value = p0});
            //         _manager.AddComponentData(bulletArr[i], new RotateDeg {Value = rot});
            //     }
            //
            //     bulletArr.Dispose();
            //     break;
            // case 1:
            //     _manager.Instantiate(_pinkBulletE, bulletArr);
            //     for (int i = 0; i < _amount; i++)
            //     {
            //         _manager.SetEnabled(bulletArr[i], false);
            //     }
            //
            //     for (int i = 0; i < _amount; i++)
            //     {
            //         _manager.SetEnabled(bulletArr[i], true);
            //         p0 = Caster.owner.transform.position;
            //         float rot = r.NextFloat(-50, 50);
            //         _manager.SetComponentData(bulletArr[i], new Translation {Value = p0});
            //         _manager.AddComponentData(bulletArr[i], new RotateDeg {Value = rot});
            //         yield return null;
            //     }
            //
            //     bulletArr.Dispose();
            //     break;
            //
            // case 2:
            //     _manager.Instantiate(_pinkBullet2E, bulletArr);
            //
            //     for (int i = 0; i < _amount; i++)
            //     {
            //         p0 = Caster.owner.transform.position;
            //         float rot = r.NextFloat(-30, 30);
            //         _manager.SetComponentData(bulletArr[i], new Translation {Value = p0});
            //         _manager.AddComponentData(bulletArr[i], new RotateDeg {Value = rot});
            //         _manager.AddComponentData(bulletArr[i], new DeadTime {Value = deadTime});
            //     }
            //
            //     bulletArr.Dispose();
            //     break;
        }


        yield return null;
    }
}