using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal: ScriptableObject
{
    protected Unit _unit;

    public Goal()
    {

    }

    public void Initialize()
    {

    }

    public void FixedUpdate()
    {

    }

    protected abstract void OnFixedUpdate();
}
