using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] SimpleParticle particle;
    public Team team;
    float life = 2;
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * 10, ForceMode2D.Impulse);
    }
    
    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0)
        {
            spawnParticle(Color.gray);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        destroyBullet();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var u = other.GetComponent<Unit>();
        if (u != null)
        {
            if (u.getTeam() == team) return;
            u.hurt(0.1f);
            destroyBullet();
            return;
        }
        if (life > 1.9)//Баррикаду впритык легко простреливать
        {
            return;
        }
        if (other.GetComponent<Barricade>()&&Random.Range(0,2)==0)
        {
            spawnParticle(Color.cyan);
            return;
        }
        destroyBullet();
    }

    void spawnParticle(Color color)
    {
        var p = Instantiate(particle, transform.position, Quaternion.identity);
        p.renderer.color = color;
    }

    void destroyBullet()
    {
        spawnParticle(Color.red);
        Destroy(gameObject);
    }
}
