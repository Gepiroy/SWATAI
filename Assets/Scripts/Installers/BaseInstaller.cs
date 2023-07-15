using UnityEngine;
using Zenject;

public class BaseInstaller : MonoInstaller
{
    [SerializeField] private SimpleParticle _particle;
    [SerializeField] private HidingPoint _hidingPoint;
    
    public override void InstallBindings()
    {
        //var particle = Container.InstantiatePrefabForComponent<SimpleParticle>(_particle);
        //Debug.Log("particle=" + particle + "; _particle = " + _particle);
        Container
            .Bind<SimpleParticle>()
            .FromInstance(_particle)
            .AsSingle();
        var spawner = Container.Instantiate<ParticleSpawner>();
        Container
            .Bind<ParticleSpawner>()
            .FromInstance(spawner)
            .AsSingle();
        Container
            .Bind<HidingPoint>()
            .FromInstance(_hidingPoint)
            .AsSingle();
    }
}