using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAIMovement : MonoBehaviour
{
    [SerializeField] private PathNode _PathNode;

    private NavMeshAgent _Agent;
    private float _Tolerance = 60.0f;
    private Coroutine _MoveCoroutine;

    public void Initialize(float mvSpd, PathNode pathStartNode)
    {
        _Agent = GetComponent<NavMeshAgent>();
        _Agent.speed = mvSpd;
        _Tolerance = _Agent.speed;
        _PathNode = pathStartNode;
    }

    private void Awake()
    {
        _Agent = GetComponent<NavMeshAgent>();
    }

    public void SetSpeed(float speed)
    {
        _Agent.speed = speed;
    }

    private void Update()
    {
        if (_PathNode == null || !_Agent.enabled)
        {
            return;
        }

        if (Vector3.Distance(transform.position, _PathNode.SamplePoint()) < _Tolerance)
        {
            _PathNode = _PathNode.GetNextNode();
            if (_PathNode == null)
            {
                return;
            }
        }

        _Agent.SetDestination(_PathNode.SamplePoint());
    }

    public void Pause()
    {
        _Agent.enabled = false;
    }

    public void Resume()
    {
        _Agent.enabled = true;
    }
}