using System;
using UnityEngine;

namespace Core
{
    public class PlayerUnit : MonoBehaviour
    {
        private float _currentGravityForce;
        public Rigidbody2D Rigidbody;
        public PlayerUnit ChildUnit;
        public Action<PlayerUnit> OnDamage;
        public bool isHold;
        public bool isDead;
        public Rigidbody2D ParentRigidbody;
        public Shooter Shooter;
        private void Start() { }

        private void Awake()
        {
            _currentGravityForce = Player.GravityForce;
        }

        public void SetHold(Rigidbody2D lastUnitRigidbody)
        {
            ParentRigidbody = lastUnitRigidbody;
            isHold = true;
            _currentGravityForce = 0;
        }

        public void Release()
        {
            isHold = false;
            isDead = true;
            _currentGravityForce = Player.GravityForce;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Obstacle obstacle))
            {
                if (isDead) return;
                OnDamage?.Invoke(this);
                isDead = true;
            }
            else if (other.gameObject.TryGetComponent(out EnemyMovement enemy))
            {
                if (isDead) return;
                OnDamage?.Invoke(this);
                isDead = true;
            }
        }

        private void FixedUpdate()
        {
            if (isDead) transform.position += Vector3.down * (_currentGravityForce * Time.fixedDeltaTime);
        }
    }
}