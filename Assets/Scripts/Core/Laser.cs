using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private void Awake()
    {
        StopPlaying();
    }

    void StopPlaying()
    {
     gameObject.SetActive(false);   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("laser");
        if (other.TryGetComponent(out EnemyMovement _))
        {
            AudioManager.instance.Play("EnemyDeath");
            Destroy(other.gameObject);
        }
    }
}
