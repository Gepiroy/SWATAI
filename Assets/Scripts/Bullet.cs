using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{

    [Inject] private ParticleSpawner _particleSpawner;
    
    public Team team;
    float life = 2;
    float height = 1.5f;
    float _heightDirection=0;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * 20, ForceMode2D.Impulse);
        _heightDirection = Random.Range(-1f, 1f);
    }

    private void FixedUpdate()
    {
        height += _heightDirection*Time.fixedDeltaTime;
    }

    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0)
        {
            _particleSpawner.SpawnParticle(transform.position, Color.gray, 2f);
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
            if (u.GetTeam() == team) return;
            if (height > u.height) return;
            u.Hurt(0.1f);
            destroyBullet();
            return;
        }

        var barricade = other.GetComponent<Barricade>();

        if (barricade!=null && height > 1)
        {
            _particleSpawner.SpawnParticle(transform.position, Color.cyan, 2f);
            return;
        }
        destroyBullet();
    }

    void destroyBullet()
    {
        _particleSpawner.SpawnParticle(transform.position, Color.red, 2f);
        Destroy(gameObject);
    }
}
