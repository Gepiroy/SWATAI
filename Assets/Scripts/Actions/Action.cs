using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action: ScriptableObject
{
    public float ExecutionTime; //–аботает при поиске, по факту акшн может длитьс€ сколь угодно до момента завершени€.

    protected float _time;
    protected Unit _unit;

    public Action(float executionTime)
    {
        ExecutionTime = executionTime;
    }

    public void Initialize(Unit unit)
    {
        _time = ExecutionTime;
        _unit = unit;
    }

    public abstract void Start();

    public void FixedUpdate()
    {
        _time -= Time.fixedDeltaTime;
        OnFixedUpdate();
        if(_time <= 0)
        {
            //TODO something.
        }
    }

    protected abstract void OnFixedUpdate();
}
