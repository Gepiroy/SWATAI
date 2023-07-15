using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Weapon: MonoBehaviour
{
    [SerializeField] Unit unit;

    [SerializeField] GameObject bulletPrefab;

    [Inject] DiContainer _diContainer;

    float cooldown = 0;
    float setCooldown = 0.5f;
    float baseSpread = 5f;
    
    int maxAmmo = 17;
    int ammo = 17;

    private void Update()
    {
        cooldown -= Time.deltaTime;
    }

    public bool tryShoot(Vector3 to, float spreadFromUnit)
    {
        if (cooldown <= 0)
        {
            shoot(to, spreadFromUnit);
            return true;
        }
        return false;
    }

    private void shoot(Vector3 to, float spreadFromUnit)
    {
        GameObject bullet = _diContainer.InstantiatePrefab(bulletPrefab, transform.position, Quaternion.identity, null);
        Bullet b = bullet.GetComponent<Bullet>();
        b.team = unit.GetTeam();
        Vector3 dir = (to-transform.position).normalized;
        float rot = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        float spread = spreadFromUnit * baseSpread;
        bullet.transform.Rotate(0, 0, rot+Random.Range(-spread, spread));
        cooldown = setCooldown;
    }
}
