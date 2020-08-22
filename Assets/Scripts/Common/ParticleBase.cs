using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBase : MonoBehaviour
{
    [SerializeField] float objectLifetime = 3f;


    // sets to destroy, need to learn how to object pool to stop using this
    private void Awake()
    {
        Destroy(this, objectLifetime);
    }
}
