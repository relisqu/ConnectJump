using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int EnemySpeed;
    public float PlayerDistance;
    public Transform Player;
    private bool _playerNear;

    void FixedUpdate()
    {
        if (_playerNear)
        {
            transform.position += Vector3.left * (EnemySpeed * Time.deltaTime);
        }
        else if (Mathf.Abs(Player.position.x - transform.position.x) <= PlayerDistance)
            _playerNear = true;
        
    }
}