using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitEvent : MonoBehaviour
{ 
    public static UnityEvent OnPlayerHit = new();
    public static UnityEvent OnEnemyHit = new();

    public static void SendPlayerHit()
    {
        OnPlayerHit.Invoke();
    }

    public static void SendEnemyHit()
    {
        OnEnemyHit.Invoke();
    }
}
