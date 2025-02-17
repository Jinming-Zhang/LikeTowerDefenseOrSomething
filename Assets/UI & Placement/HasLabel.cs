using UnityEngine;

public class HasLabel : PlayerObjectHealth
{
    public RectTransform labelPrefab; // The object will spawn this in on Start.
    RectTransform label;
    public bool isHPBar;

    //public Vector3 offset;

    public new void Start()
    {
        base.Start();

        Canvas c = FindFirstObjectByType<Canvas>();
        label = Instantiate(labelPrefab, c.transform);

        FloatingHP bar = label.GetComponent<FloatingHP>();
        if(bar != null) { bar.AssignHP(this); }
    }
}
