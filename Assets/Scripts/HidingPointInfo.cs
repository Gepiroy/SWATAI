using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPointInfo
{
    public HidingPoint point;
    private Team team;

    public HidingPointInfo(HidingPoint point, Team team)
    {
        this.point = point;
        this.team = team;
    }

    public float threat=1;
    public float value=1;
    public Unit occupant;
    
    public void setOccupant(Unit unit)
    {
        if(occupant != null) { occupantLeaved(Vector3.up); }
        occupant = unit;
        if (unit == null) return;
        occupant.getNav().onNavTargetChanged += occupantLeaved;
    }

    void occupantLeaved(Vector3 v)
    {
        occupant.getNav().onNavTargetChanged -= occupantLeaved;
        occupant = null;
    }
}
