using UnityEngine;
using Zenject;

public class ParticleSpawner
{
    [Inject] SimpleParticle _particle;

    public void SpawnParticle(Vector3 position, Color color, float time)
    {
        var p = Object.Instantiate(_particle, position, Quaternion.identity);

        p.Renderer.color = color;
        p.lifetime = time;
    }
}
