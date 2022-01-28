//code by 赫斯基皇
//https://space.bilibili.com/455965619
//https://github.com/Heskey0

using System;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class pinkBulletMono : MonoBehaviour
{
    private Transform _target;
    private float speed = 12;

    public float rotateDeg;
    private void Start()
    {
        rotateDeg= Shoot.r.NextFloat(-40, 40);
        Shoot.bulletAmount += 1;
        _target = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    private void Update()
    {
        transform.position += (Vector3)math.mul(transform.rotation, new float3(1, 0, 0)) * Time.deltaTime *
                              Shoot.r.NextFloat(1, 12);
        Track();
    }


    private void Track()
    {
        var dir = _target.position - transform.position;
        float angle = math.degrees(math.atan2(dir.y, dir.x)) + rotateDeg;
                
        var tmpRot = transform.rotation;
        tmpRot = Quaternion.Euler(new float3(0, 0, angle));
        transform.rotation = tmpRot;
                
        //销毁
        var dis = math.length(dir);
        if (dis < 0.3f)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Shoot.bulletAmount -= 1;
    }
}