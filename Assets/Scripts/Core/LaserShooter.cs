using System;
using UnityEngine;

namespace Core
{
    public class LaserShooter : Shooter
    {
        public override void MakeShootBullet()
        {
            //Instantiate(bullet, shootingPosition, Quaternion.identity);
              bullet.SetActive(true);
              AudioManager.instance.Play("Laser");
              
        }

        private void Update()
        {
            bullet.transform.position = shootingPosition+transform.position+ bullet.transform.localScale.x/2*Vector3.right;
        }

       
    }
}