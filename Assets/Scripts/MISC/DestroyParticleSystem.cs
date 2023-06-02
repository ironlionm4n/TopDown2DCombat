using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MISC
{
    public class DestroyParticleSystem : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_particleSystem && !_particleSystem.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}