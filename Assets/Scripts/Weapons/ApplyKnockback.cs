using System;
using UnityEngine;

namespace Weapons
{
    public class ApplyKnockback : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void GetKnockedBack(Transform damageSource, float knockBackThrust)
        {
            var force = (Vector2) (transform.position - damageSource.position).normalized
                        * (knockBackThrust * _rigidbody2D.mass);

            Debug.Log(force);
            _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
        }
    }
}