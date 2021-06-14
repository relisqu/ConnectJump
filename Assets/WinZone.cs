using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    public GameObject zone;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Player _))
        {
            zone.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _))
        {
            zone.SetActive(true);
        }
    }
}
