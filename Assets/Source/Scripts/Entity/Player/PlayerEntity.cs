using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : EntityBase
{
    [SerializeField] private float immortalTimer = 1f;
    private float immortalStart = 0;
    private void Start()
    {
        onDeath += (EntityBase a) => immortalStart = Time.time;   
    }
    private void OnDestroy()
    {
        onDeath -= (EntityBase a) => immortalStart = Time.time;
    }
    private void Update()
    {
        if (Time.time >= immortalStart + immortalTimer)
        {
            invincible = false;
        }
    }
}
