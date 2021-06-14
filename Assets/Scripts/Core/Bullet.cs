using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed; 
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.right * (Speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        print(other.name);
        if (other.TryGetComponent(out Obstacle _))
        {
            Destroy(gameObject);
        }
        
        if (other.TryGetComponent(out EnemyMovement movement))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Obstacle _))
        {
            Destroy(gameObject);
        } 
        if (other.gameObject.TryGetComponent(out EnemyMovement movement))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            AudioManager.instance.Play("EnemyDeath");
        }
    }
}
