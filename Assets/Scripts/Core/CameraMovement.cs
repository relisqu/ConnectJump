using System;
using UnityEngine;

namespace Core
{
    public class CameraMovement : MonoBehaviour
    {
        public Transform Player;
        public float Offset;
        private void LateUpdate()
        {
            var previousPosition = transform.position;
            transform.position = new Vector3(Player.transform.position.x+Offset, previousPosition.y,previousPosition.z);
        }
    }
}