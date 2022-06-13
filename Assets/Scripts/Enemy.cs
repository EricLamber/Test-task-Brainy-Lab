using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float MoveSpeed;
    [SerializeField] private float RotationSpeed;

    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform BulletTransform;

    //For ray
    [SerializeField] private LayerMask EnviromentLayer;
    
    //CoolDown for fire
    [SerializeField] private float FireCD;
    private float time;

    private Rigidbody2D rb;

    //For rotation
    private float rot;

    //For moving
    private float velocityx = -1;
    private float velocityy = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovingLogic();
    }

    private void Update()
    {
        Rotation();
        Fire();
    }

    private void MovingLogic()
    {
        if (Physics2D.Raycast(transform.position, new Vector2(0, 1), .7f, EnviromentLayer) ||
            Physics2D.Raycast(transform.position, new Vector2(0, -1), .7f, EnviromentLayer))
        {
            velocityy *= -1;
            rb.velocity = new Vector2(velocityx * MoveSpeed, velocityy * MoveSpeed);
        }
        else if (Physics2D.Raycast(transform.position, new Vector2(1, 0), .7f, EnviromentLayer) ||
                 Physics2D.Raycast(transform.position, new Vector2(-1, 0), .7f, EnviromentLayer))
        {
            velocityx *= -1;
            rb.velocity = new Vector2(velocityx * MoveSpeed, velocityy * MoveSpeed);
        }
        else if (Physics2D.Raycast(transform.position, new Vector2(1, 1), .7f, EnviromentLayer) ||
                 Physics2D.Raycast(transform.position, new Vector2(-1, -1), .7f, EnviromentLayer) ||
                 Physics2D.Raycast(transform.position, new Vector2(-1, 1), .7f, EnviromentLayer) ||
                 Physics2D.Raycast(transform.position, new Vector2(1, -1), .7f, EnviromentLayer))
        {
            velocityy *= -1;
            velocityx *= -1;
            rb.velocity = new Vector2(velocityx * MoveSpeed, velocityy * MoveSpeed);
        }
        else
        {
            rb.velocity = new Vector2(velocityx * MoveSpeed, velocityy * MoveSpeed);
        }
    }

    private void Rotation()
    {
        rot = 360 + (Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90);
        if (rb.velocity.x == 0 && rb.velocity.y == 0)
            rb.rotation = rb.rotation;
        else if (rb.rotation < rot)
            rb.rotation += RotationSpeed;
        else if (rb.rotation > rot)
            rb.rotation -= RotationSpeed;
    }
    
    private bool Aim(Vector2 position, Vector2 direction, int reflectnum)
    {
        RaycastHit2D hit1 = Physics2D.Raycast(position, direction, 48f, EnviromentLayer);
        
        switch (hit1.collider.tag)
        {
            case "Player":
                return true;
            case "Terrain":
                if (reflectnum > 0)
                {
                    position = hit1.point;
                    direction = Vector2.Reflect(direction, hit1.normal);
                    return Aim(position, direction, reflectnum - 1);
                }else
                    return false;
            default:
                return false;
        }
    }

    private void Fire()
    {

        if (time <= 0 && Aim(transform.position, transform.up, 2))
        {
            if(Instantiate(Bullet, BulletTransform.position, transform.rotation))
            time = FireCD;
        }
        else if (time <= FireCD && time > 0)
            time -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag is "Bullet")
        {
            HitEvent.SendPlayerHit();
            Destroy(other.gameObject);
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            Destroy(gameObject);
        }
    }
}
