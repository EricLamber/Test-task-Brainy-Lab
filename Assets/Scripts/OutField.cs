using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutField : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Bullet")
        Destroy(other.gameObject);
    }
}
