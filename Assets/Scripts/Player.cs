using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float RotationSpeed;

    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform BulletTransform;

    //Inputs
    private float Hinput;
    private float Vinput;
    private float RotationInput;

    //Components
    private Rigidbody2D rb;

    //CD for fire
    [SerializeField] private float FireCD;
    private float time;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        PlayerInput();
        Move();
        Fire();
    }

    private void PlayerInput()
    {
        Hinput = Input.GetAxis("Horizontal");
        Vinput = Input.GetAxis("Vertical");
        RotationInput = Input.GetAxis("Rotation");
    }

    private void Move()
    {
            rb.velocity = new Vector2(Hinput * MoveSpeed, Vinput * MoveSpeed);
        
        rb.rotation += RotationInput * RotationSpeed;
    }

    private void Fire()
    {
        if (time <= 0 && Input.GetButtonDown("Fire"))
        {
            if (Instantiate(Bullet, BulletTransform.position, transform.rotation))
                time = FireCD;
        }
        else if (time <= FireCD && time > 0)
            time -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag is "Bullet")
        {
            HitEvent.SendEnemyHit();
            Destroy(other.gameObject);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            Destroy(gameObject);
        }
    }
}
