using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAIMovement : MonoBehaviour
{
    private NavMeshAgent _Agent;
    [SerializeField] private PathNode _PathNode;
    [SerializeField] private float _Tolerance = 1.0f;
    [SerializeField] private bool _MoveOnAwake = true;


    private Coroutine _MoveCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _Agent = GetComponent<NavMeshAgent>();
        if (_PathNode != null && _MoveOnAwake)
        {
            FollowPath();
        }
    }

    private void Update()
    {
        if (_PathNode == null)
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

    public void AssignPath(PathNode head)
    {
        _PathNode = head;
    }

    public void FollowPath()
    {
        // StopAllCoroutines();
        // StartCoroutine(MoveCr());
    }

    public void Pause()
    {
        _Agent.enabled = false;
    }

    public void Resume()
    {
        _Agent.enabled = true;
    }

    // private IEnumerable MoveCr()
    // {
    //     yield return null;
    // }
}