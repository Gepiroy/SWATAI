using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    public delegate void NavTargetChanged(Vector3 newTarget);
    public event NavTargetChanged OnNavTargetChanged;

    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = gameObject.AddComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        //agent.speed= 1.0f;
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNavTarget(Vector3 target)
    {
        _agent.SetDestination(target);
        OnNavTargetChanged?.Invoke(target);
    }

    public bool IsMoving()
    {
        return _agent.hasPath;
    }

    public void SetSpeed(float speed)
    {
        _agent.speed = speed;
    }
}
