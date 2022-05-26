using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public delegate void Damage();
    public static event Damage OnTakeDamage;

    public GameObject sprite;
    // public Transform pointA;
    // public Transform pointB;

    private void Start()
    {
        //sprite.transform.DOMove(pointA.position, 1);
    }

    private void Update()
    {
        // if (sprite.transform.position == pointA.position)
        //     sprite.transform.DOMove(pointB.position, 1);
        // if (sprite.transform.position == pointB.position) 
        //     sprite.transform.DOMove(pointA.position, 1);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (OnTakeDamage != null) OnTakeDamage();
    }
}
