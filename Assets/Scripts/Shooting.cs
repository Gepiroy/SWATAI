using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting: MonoBehaviour
{
    [SerializeField] Unit unit;

    [SerializeField] GameObject bulletPrefab;

    float cooldown = 0;
    float setCooldown = 0.5f;
    
    int maxAmmo = 17;

    private void Update()
    {
        cooldown -= Time.deltaTime;
    }

    public bool tryShoot(Vector3 to, float recoil)
    {
        if (cooldown <= 0)
        {
            shoot(to, recoil);
            return true;
        }
        return false;
    }

    private void shoot(Vector3 to, float recoil)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet b = bullet.GetComponent<Bullet>();
        b.team = unit.getTeam();
        Vector3 dir = (to-transform.position).normalized;
        float rot = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        bullet.transform.Rotate(0, 0, rot+Random.Range(-recoil, recoil));
        cooldown = setCooldown;
    }
}
