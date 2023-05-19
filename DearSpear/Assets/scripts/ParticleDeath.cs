using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDeath : MonoBehaviour
{
    ParticleSystem particles;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (particles.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
