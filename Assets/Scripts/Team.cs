using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour, Teamable, UnitHolder
{
    public List<HidingPointInfo> hidingPoints { get; } = new();
    [SerializeField] public Color color = Color.white;
    void Start()
    {
        Teams.regTeam(this);
        foreach (HidingPoint p in FindObjectsByType<HidingPoint>(FindObjectsSortMode.None))
        {
            hidingPoints.Add(new HidingPointInfo(p, this));
        }
    }
    
    void Update()
    {
        
    }

    public Team getTeam()
    {
        return this;
    }

    public Group[] getGroups()
    {
        return GetComponentsInChildren<Group>();
    }

    public List<Unit> getUnits()
    {
        List<Unit> ret = new List<Unit>();
        foreach (Group g in getGroups())
        {
            ret.AddRange(g.getUnits());
        }
        return ret;
    }
}
