using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float BulletSpeed;
    [SerializeField] private LayerMask collisionMask;

    private float rot;
    private Vector2 reflectdir;

    private void FixedUpdate()
    {
        BulletFly();
    }

    private void BulletFly()
    {
        transform.Translate(BulletSpeed * Time.deltaTime * Vector2.up);
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        reflectdir = Vector2.Reflect(transform.up, coll.contacts[0].normal);
        rot = Mathf.Atan2(reflectdir.y, reflectdir.x) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, rot);
    }
}
