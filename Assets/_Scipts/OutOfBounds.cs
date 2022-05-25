using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public delegate void Damage();
    public static event Damage OnTakeDamage;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (OnTakeDamage != null) OnTakeDamage();
    }
}
