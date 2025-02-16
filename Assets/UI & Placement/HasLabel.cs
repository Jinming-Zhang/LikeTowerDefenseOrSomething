using UnityEngine;

public class HasLabel : MonoBehaviour
{
    public RectTransform labelPrefab; // The object will spawn this in on Start.
    RectTransform label;
    public Vector3 offset;

    public void Start()
    {
        Canvas c = FindFirstObjectByType<Canvas>();
        label = Instantiate(labelPrefab, c.transform);
    }

    private void Update()
    {
        label.position = Camera.main.WorldToScreenPoint(transform.position + offset);
    }
}
