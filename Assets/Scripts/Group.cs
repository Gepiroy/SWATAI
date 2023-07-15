using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour, ITeamable, IUnitHolder
{
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public List<Unit> GetUnits()
    {
        return new List<Unit>(GetComponentsInChildren<Unit>());
    }

    public Team GetTeam()
    {
        return transform.parent.GetComponent<Team>();
    }
}
