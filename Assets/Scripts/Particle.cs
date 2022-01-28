using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    void Start()
    {
        Invoke("Func",0.5f);
    }

    void Func()
    {
        Destroy(gameObject);
    }
}
