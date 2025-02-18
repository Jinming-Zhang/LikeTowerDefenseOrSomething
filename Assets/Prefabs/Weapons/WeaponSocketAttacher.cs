using UnityEngine;

public class WeaponSocketAttacher : MonoBehaviour
{
    [SerializeField] private Transform _Target;
    [SerializeField] private Vector3 _Offset;

    // Update is called once per frame
    void Update()
    {
        if (_Target != null)
        {
            transform.position = _Target.position + _Offset;
        }
    }
}