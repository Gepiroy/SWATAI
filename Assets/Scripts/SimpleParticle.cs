using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleParticle : MonoBehaviour
{
    [SerializeField] public SpriteRenderer renderer;
    public double lifetime = 2;
    void Start()
    {
        
    }
    
    void Update()
    {
        lifetime-=Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
