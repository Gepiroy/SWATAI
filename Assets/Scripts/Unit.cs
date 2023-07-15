using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Unit : MonoBehaviour, ITeamable
{
    [SerializeField] SpriteRenderer body;
    [SerializeField] Weapon gun;
    [SerializeField] HealthBar healthBar;
    public float health = 1.0f;
    public float height = 2.0f;
    void Start()
    {
        body.color=GetTeam().color;
        var act = ScriptableObject.CreateInstance<ActionShootFromCover>();
    }

    private Unit aimingTarget;
    
    private Action _action;

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //if (!GetNav().IsMoving())
        {
            SetAimingTarget(ClosestEnemy());
            if (Random.Range(0, 100) == 1)
            {
                ShootAtTarget();
            }
            if (Random.Range(0, 1000) == 1)
            {
                SetAiming(!IsAiming);
            }
            if (Random.Range(0, 1000) == 1)
            {
                SetCrouching(!IsCrouching);
            }
            if (Random.Range(0, 1000) == 1)
            {
                var hide = FindBestHidingPoint();
                GetNav().SetNavTarget(hide.point.transform.position);
                hide.setOccupant(this);
            }
        }
    }

    HidingPointInfo FindBestHidingPoint()
    {
        return GetTeam().hidingPoints[Random.Range(0, GetTeam().hidingPoints.Count)];
        /*HidingPointInfo bestPoint = null;
        double minDist = 10000;
        foreach (HidingPointInfo hpi in GetTeam().hidingPoints)
        {
            if (hpi.occupant != null) continue;
            double dist = Vector3.Distance(hpi.point.transform.position, transform.position);
            if (dist<minDist)
            {
                minDist = dist;
                bestPoint = hpi;
            }
        }
        return bestPoint;*/
    }

    void ShootAtTarget()
    {
        ShootToPoint(aimingTarget.transform.position);
    }

    void LookAtTarget(Vector3 target)
    {
        //Вектор направления
        var dir = target - transform.position;
        //Из него берём радиан 2д-шный
        float rot = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        //Обнуляем положение взгляда
        body.transform.rotation = Quaternion.identity;
        //Поворачиваем чела на эти радианы
        body.transform.Rotate(0, 0, rot);
    }

    void ShootToPoint(Vector3 point)
    {
        LookAtTarget(point);

        float spread = 1;
        if (IsMoving()) spread *= 2;
        if (IsAiming) spread /= 2;
        gun.tryShoot(point, spread);
    }

    Unit ClosestEnemy()
    {
        var enemies = Teams.unitEnemiesOf(this);
        enemies.Sort((a, b) => { return Vector3.Distance(a.transform.position, transform.position) > Vector3.Distance(b.transform.position, transform.position)?1:-1; });
        return enemies[0];
    }

    public bool IsMoving()
    {
        return GetNav().IsMoving();
    }

    public void SetAimingTarget(Unit target)
    {
        aimingTarget = target;
    }

    public bool IsAiming { get; private set; } = false;

    public void SetAiming(bool toSet)
    {
        IsAiming = toSet;
        Vector3 pos = gun.transform.localPosition;
        if (IsAiming)
        {
            pos.x = 0;
            pos.y = 0.5f;
        }
        else
        {
            pos.x = 0.37f;
            pos.y = 0.38f;
        }
        gun.transform.localPosition = pos;
        UpdateSpeed();
    }

    public bool IsCrouching { get; private set; } = false;

    public void SetCrouching(bool toSet)
    {
        IsCrouching = toSet;
        var renderer = body.GetComponent<SpriteRenderer>();
        var color = renderer.color;
        color.a = IsCrouching ? 0.5f : 1f;
        renderer.color = color;
        UpdateSpeed();
    }

    public void UpdateSpeed()
    {
        float speed = 5f;
        if (IsAiming) speed /= 2;
        if (IsCrouching) speed /= 2;
        GetNav().SetSpeed(speed);
    }

    public Team GetTeam()
    {
        return transform.parent.parent.GetComponent<Team>();
    }
    public AgentMovement GetNav()
    {
        return GetComponent<AgentMovement>();
    }

    public void Hurt(float damage)
    {
        AddHealth(-damage);
        if (health <= 0)
        {
            AddHealth(1);
            //Destroy(gameObject);
        }
    }
    public void AddHealth(float toAdd)
    {
        health += toAdd;
        healthBar.SetProgress(health);
    }
}
