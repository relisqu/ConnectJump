using System.Collections;
using UnityEngine;

namespace Core
{
    public class TurretShooter : Shooter
    {
        public int BulletCount;
        public float PauseBetweenBullets;
        public override void MakeShootBullet()
        {
            StartCoroutine(CreateBullet());
        }

        IEnumerator CreateBullet()
        {
            print("AAA");
            for (var i = 0; i < BulletCount; i++)
            {
                
                Instantiate(bullet, shootingPosition+transform.position, Quaternion.identity);
                AudioManager.instance.Play("Shot");
                yield return new WaitForSeconds(PauseBetweenBullets);
            }
            
        }

    }
}