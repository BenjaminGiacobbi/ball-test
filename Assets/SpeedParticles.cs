using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBase : MonoBehaviour
{
    [SerializeField] float objectLifetime = 3f;

    private void Awake()
    {
        Destroy(this, objectLifetime);
    }
}
