using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAIMovement : MonoBehaviour
{
    [SerializeField] private PathNode _PathNode;
    [SerializeField] private AudioSource _AudioSource;


    private NavMeshAgent _AgentIns;

    private NavMeshAgent _Agent
    {
        get
        {
            if (_AgentIns == null)
            {
                _AgentIns = GetComponent<NavMeshAgent>();
            }

            return _AgentIns;
        }
    }

    [SerializeField] private float _Tolerance = 20.0f;
    private Coroutine _MoveCoroutine;

    public void Initialize(float mvSpd, PathNode pathStartNode)
    {
        _Agent.speed = mvSpd;
        _PathNode = pathStartNode;
    }

    public void SetSpeed(float speed)
    {
        _Agent.speed = speed;
    }

    private void Start()
    {
        _AudioSource.Play();
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
                // _AudioSource.Stop();
                return;
            }
        }

        _Agent.SetDestination(_PathNode.SamplePoint());
    }

    public void Pause()
    {
        _Agent.enabled = false;
        _AudioSource.Pause();
    }

    public void Resume()
    {
        _Agent.enabled = true;
        _AudioSource.UnPause();
    }

    public PathNode GetCurrentPathNode()
    {
        return _PathNode;
    }
}