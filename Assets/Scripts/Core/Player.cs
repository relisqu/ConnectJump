using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    public class Player : MonoBehaviour
    {
        public float MovementSpeed;
        public float TapForce;
        public static float GravityForce = 4.9f;
        public Rigidbody2D Rigidbody;
        private PlayerUnit unit;
        public Vector3 offset;
        public Vector3 childOffset;
        public float InvincibilityDuration;
        private bool isDead;
        private bool isInvinsible;
        public GameObject FinalScreen;

        
        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Obstacle obstacle))
            {
                GetDamage();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (isDead) return;

            if (other.gameObject.TryGetComponent(out PlayerUnit newUnit))

            {
                if (newUnit.isHold || newUnit.isDead) return;
                var lastUnit = GetLastUnit();
                newUnit.OnDamage += GetDamageAtSegment;
                if (lastUnit == null)
                {
                    newUnit.transform.parent = transform;
                    newUnit.transform.position = transform.position + offset;
                    newUnit.SetHold(Rigidbody);

                    unit = newUnit;
                }
                else
                {
                    Rigidbody.velocity = new Vector2(0, 0);
                    Rigidbody.AddForce(Vector2.up * TapForce*0.5f, ForceMode2D.Impulse);
                    transform.position += Vector3.right * (MovementSpeed * Time.fixedDeltaTime);
                    newUnit.transform.parent = lastUnit.transform;
                    newUnit.transform.position = lastUnit.transform.position + childOffset;
                    newUnit.SetHold(lastUnit.Rigidbody);
                    lastUnit.ChildUnit = newUnit;
                }
            }
            else if (other.gameObject.TryGetComponent(out Obstacle obstacle))
            {
                GetDamage();
            }
            else if (other.gameObject.TryGetComponent(out EnemyMovement enemy))
            {
                GetDamage();
            }
        }

        void GetDamage()
        {
            if (!isInvinsible) GetDamageAtSegment(GetLastUnit());
        }

        void GetDamageAtSegment(PlayerUnit dropUnit)
        {
            DropUnit(dropUnit);
            StartCoroutine(MakePlayerInvinsible());
        }

        private void Awake()
        {
        }

        void DropUnit(PlayerUnit dropUnit)
        {
            if (dropUnit == null)
            {
                Die();
            }
            else
            {
                AudioManager.instance.Play("Damage");
                dropUnit.transform.parent = null;
                dropUnit.Release();
                var currentUnit = unit;
                if (currentUnit == dropUnit)
                {
                    unit = null;
                }
                else
                {
                    while (currentUnit.ChildUnit != dropUnit)
                    {
                        currentUnit = currentUnit.ChildUnit;
                    }

                    currentUnit.ChildUnit = null;
                }

                dropUnit.OnDamage -= GetDamageAtSegment;
            }
        }

        IEnumerator MakePlayerInvinsible()
        {
            isInvinsible = true;
            yield return new WaitForSeconds(InvincibilityDuration);
            isInvinsible = false;
        }

        void Die()
        {
            AudioManager.instance.Play("Die");
            isDead = true;
            print("Player died");
        }

        public PlayerUnit GetLastUnit()
        {
            if (unit == null)
            {
                return null;
            }

            var currentUnit = unit;
            while (currentUnit.ChildUnit != null)
            {
                currentUnit = currentUnit.ChildUnit;
            }

            return currentUnit;
        }

        void Update()
        {
            if (isDead) return;
            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody.velocity = new Vector2(0, 0);
                Rigidbody.AddForce(Vector2.up * TapForce, ForceMode2D.Impulse);
                transform.position += Vector3.right * (MovementSpeed * Time.fixedDeltaTime);
                AudioManager.instance.Play("Jump");
                if (unit == null) { }
                else
                {
                    StartCoroutine(unit.Shooter.Shoot());
                    var nextUnit = unit.ChildUnit;
                    while (nextUnit != null)
                    {
                        StartCoroutine(nextUnit.Shooter.Shoot());
                        nextUnit = nextUnit.ChildUnit;
                    }
                }
            }
        }


        private void FixedUpdate()
        {
            if (isDead)
            {
                FinalScreen.gameObject.SetActive(true);
                return;
            }

            Rigidbody.AddForce(Vector2.down * GravityForce, ForceMode2D.Force);
            transform.position += Vector3.right * (MovementSpeed * Time.fixedDeltaTime);
        }
    }
}