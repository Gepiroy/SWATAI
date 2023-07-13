using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour, Teamable, UnitHolder
{
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public List<Unit> getUnits()
    {
        return new List<Unit>(GetComponentsInChildren<Unit>());
    }

    public Team getTeam()
    {
        return transform.parent.GetComponent<Team>();
    }
}
