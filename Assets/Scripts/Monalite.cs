using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITeamable
{
    public Team GetTeam();
}
public interface IUnitHolder
{
    public List<Unit> GetUnits();
}