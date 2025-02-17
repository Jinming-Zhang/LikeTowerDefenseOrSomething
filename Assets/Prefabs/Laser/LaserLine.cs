using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class LaserLine : MonoBehaviour
{
    private Transform _Start;

    private Transform _End;
    private LineRenderer _LineRenderer;

    private void Awake()
    {
        if (transform.childCount >= 2)
        {
            _Start = transform.GetChild(0);
            _End = transform.GetChild(1);
        }

        _LineRenderer = GetComponent<LineRenderer>();
        UpdateLineRendererPosition();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLineRendererPosition();
    }

    public void SetLaserPoints(Transform start, Transform end)
    {
        _Start = start;
        _End = end;
    }

    private void UpdateLineRendererPosition()
    {
        _LineRenderer.SetPosition(0, _Start.position);
        _LineRenderer.SetPosition(1, _End.position);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}