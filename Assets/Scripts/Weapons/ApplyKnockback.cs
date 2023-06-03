using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class ApplyKnockback : MonoBehaviour
    {
        [SerializeField] private float knockBackTime;
        public bool GettingKnockedBack {
            get;

        private set; 
    }
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void GetKnockedBack(Transform damageSource, float knockBackThrust)
        {
            GettingKnockedBack = true;
            var force = (Vector2) (transform.position - damageSource.position).normalized
                        * (knockBackThrust * _rigidbody2D.mass);

            _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(KnockRoutine());
        }

        private IEnumerator KnockRoutine()
        {
            yield return new WaitForSeconds(knockBackTime);
            _rigidbody2D.velocity = Vector2.zero;
            GettingKnockedBack = false;
        }
    }
}