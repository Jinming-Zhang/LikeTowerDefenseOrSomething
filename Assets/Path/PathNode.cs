using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathNode : MonoBehaviour
{
    [SerializeField] private List<PathNode> _Next = new();
    [SerializeField] private Bounds _SampleArea;

    [Header("Debug")]
    [SerializeField] private Color _PathGizmosClr = Color.cyan;

    private Vector3 _Point;

    private void Awake()
    {
        InitPath();
    }

    [ContextMenu("Init Path")]
    public void InitPath()
    {
        _Next = new();
        foreach (Transform child in transform)
        {
            PathNode p = child.GetComponent<PathNode>();
            if (p != null)
            {
                p.InitPath();
                _Next.Add(p);
            }
        }
    }


    public virtual Vector3 SamplePoint()
    {
        _Point = transform.position;
        return transform.position;
    }

    public virtual PathNode GetNextNode()
    {
        if (_Next.Count <= 0)
        {
            return null;
        }

        PathNode next = _Next[Random.Range(0, _Next.Count - 1)];
        return next;
    }

    private void OnDrawGizmosSelected()
    {
        Color old = Gizmos.color;
        Gizmos.color = _PathGizmosClr;
        foreach (PathNode node in _Next)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }

        Gizmos.color = old;
    }
}