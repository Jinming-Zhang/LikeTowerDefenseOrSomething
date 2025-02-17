using UnityEngine;

public class FloatingHP : MonoBehaviour
{
    public PlayerObjectHealth referenceHP;
    public RectTransform hpBar;
    [Space(15f)]
    public bool scaleToCameraPosition;
    [Space(7.5f)]
    public Transform scalingTransform;
    [Space(7.5f)]
    public float slidingTransformDefaultHeight;
    public float heightMultiplier;
    [Space(7.5f)]
    public Vector3 defaultScale;
    public float scaleMultiplier;

    Transform camera;

    public void Awake()
    { camera = Camera.main.transform; }

    public void AssignHP(PlayerObjectHealth poh)
    { referenceHP = poh; }

    private void Update()
    {
        if(referenceHP == null) { return; }
        transform.position = Camera.main.WorldToScreenPoint(referenceHP.transform.position);

        if (referenceHP == null) { return; }
        hpBar.localScale = new Vector3(referenceHP.health / referenceHP.maxHealth, hpBar.localScale.y, hpBar.localScale.z);

        if (scaleToCameraPosition) { CameraDistanceScale(); }
    }

    public void CameraDistanceScale()
    {
        float cameraDistance =  Vector3.Distance(transform.position, camera.position);

        transform.localScale = defaultScale * (1/cameraDistance) * scaleMultiplier;
        scalingTransform.localPosition =
            new Vector3(0f, slidingTransformDefaultHeight * heightMultiplier * cameraDistance, 0f);
    }
}
