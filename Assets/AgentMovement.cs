using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.AddComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        //agent.speed= 1.0f;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setNavTarget(Vector3 target)
    {
        agent.SetDestination(target);
        onNavTargetChanged?.Invoke(target);
    }

    public bool isMoving()
    {
        return agent.hasPath;
    }

    public void setSpeed(float speed)
    {
        agent.speed = speed;
    }

    public delegate void NavTargetChanged(Vector3 newTarget);
    public event NavTargetChanged onNavTargetChanged;
}
