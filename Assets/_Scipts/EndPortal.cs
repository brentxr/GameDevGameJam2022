using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    public delegate void EnterPortal();
    public static event EnterPortal OnEnterPortal;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (OnEnterPortal != null) OnEnterPortal();
    }
}
