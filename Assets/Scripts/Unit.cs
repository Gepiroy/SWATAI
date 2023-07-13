using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Unit : MonoBehaviour, Teamable
{
    [SerializeField] SpriteRenderer body;
    [SerializeField] Shooting gun;
    [SerializeField] HealthBar healthBar;
    public float health = 1.0f;
    void Start()
    {
        body.color=getTeam().color;
    }

    Unit target;
    
    void Update()
    {
        //if (getNav().velocity.magnitude==0)
        {
            target = closestEnemy();
            
            if (Random.Range(0, 100) == 1)
            {
                shoot(target.transform.position);
            }
            if (Random.Range(0, 1000) == 1)
            {
                aim();
            }

            if (Random.Range(0, 1000) == 1)
            {
                var hide = findBestHidingPoint();
                getNav().setNavTarget(hide.point.transform.position);
                hide.setOccupant(this);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            getNav().setNavTarget(pos);
        }
    }

    HidingPointInfo findBestHidingPoint()
    {
        HidingPointInfo bestPoint = null;
        double minDist = 10000;
        foreach (HidingPointInfo hpi in getTeam().hidingPoints)
        {
            if (hpi.occupant != null) continue;
            double dist = Vector3.Distance(hpi.point.transform.position, transform.position);
            if (dist<minDist)
            {
                minDist = dist;
                bestPoint = hpi;
            }
        }
        return bestPoint;
    }

    void shoot(Vector3 target)
    {
        var dir = target- transform.position;
        float rot = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        body.transform.rotation = Quaternion.identity;
        body.transform.Rotate(0, 0, rot);
        float recoil = 5;
        if (isMoving()) recoil *= 2;
        if (isAiming) recoil /= 2;
        gun.tryShoot(target, recoil);
    }

    Unit closestEnemy()
    {
        var enemies = Teams.unitEnemiesOf(this);
        enemies.Sort((a, b) => { return Vector3.Distance(a.transform.position, transform.position) > Vector3.Distance(b.transform.position, transform.position)?1:-1; });
        return enemies[0];
    }

    public bool isMoving()
    {
        return getNav().isMoving();
    }

    public void aim()
    {
        isAiming = !isAiming;
        Vector3 pos = gun.transform.localPosition;
        if (isAiming)
        {
            pos.x = 0;
            pos.y = 0.5f;
            gun.transform.localPosition = pos;
        }
        else
        {
            pos.x = 0.37f;
            pos.y = 0.38f;
            gun.transform.localPosition = pos;
        }
        updateSpeed();
    }

    public void updateSpeed()
    {
        float speed = 5f;
        if (isAiming) speed/=2;
        getNav().setSpeed(speed);
    }

    public bool isAiming { get; private set; } = false;

    public Team getTeam()
    {
        return transform.parent.parent.GetComponent<Team>();
    }
    public AgentMovement getNav()
    {
        return GetComponent<AgentMovement>();
    }

    public void hurt(float damage)
    {
        health -= damage;
        healthBar.setProgress(health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
