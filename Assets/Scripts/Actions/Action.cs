using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action: ScriptableObject
{
    public float ExecutionTime; //�������� ��� ������, �� ����� ���� ����� ������� ����� ������ �� ������� ����������.

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
