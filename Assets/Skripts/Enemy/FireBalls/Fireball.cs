using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _targetPosition;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        if (transform.position == _targetPosition)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyTarget(Unit unit)
    {
        _targetPosition = unit.transform.position;
    }
}
