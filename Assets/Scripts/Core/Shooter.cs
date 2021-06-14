using System.Collections;
using UnityEngine;

namespace Core
{
    public abstract class Shooter : MonoBehaviour
    {
        public Vector3 shootingPosition;
        public GameObject bullet;
        public float reload;
        private bool isAbleToShoot;
        public abstract void MakeShootBullet();

        public IEnumerator Shoot()
        {
            if(isAbleToShoot)MakeShootBullet();
            yield return DelayBetweenShots();
        }

        IEnumerator DelayBetweenShots()
        {
            isAbleToShoot = false;
            yield return new WaitForSeconds(reload);
            isAbleToShoot = true;
        }
    }
}