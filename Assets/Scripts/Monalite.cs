using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Teamable
{
    public Team getTeam();
}
public interface UnitHolder
{
    public List<Unit> getUnits();
}