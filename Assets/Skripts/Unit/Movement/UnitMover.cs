using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitMover : MonoBehaviour
{
    public Enemy EnemyTarget { get; private set; }

    private bool _isTargetReached;
    private Unit _unit;
    private float _moveSpeed;
    private Vector3 _targetPosition;

    private Animator _animator;


    private void OnEnable()
    {
        _unit.TargetChanged += ApplyNewTarget;
    }

    private void OnDisable()
    {
        _unit.TargetChanged -= ApplyNewTarget;
    }

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _moveSpeed = _unit.MoveSpeed;
        _targetPosition = transform.position;
        _isTargetReached = true;

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_unit.IsAlive)
        {
            if (EnemyTarget != null)
            {
                var heading = new Vector3(EnemyTarget.transform.position.x, transform.position.y, EnemyTarget.transform.position.z) - transform.position;
                if (heading.sqrMagnitude < _unit.AttackRange * _unit.AttackRange)
                {
                    _isTargetReached = true;
                }
            }

            if (transform.position != _targetPosition && _isTargetReached == false)
            {
                MoveToTargetPosition(_targetPosition);
                _animator.SetBool("IsTargetReached", _isTargetReached);
            }
            RotateToTargetPosition(_targetPosition);
        }
    }
    
    private void MoveToTargetPosition(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);

        if (transform.position == _targetPosition)
        {
            _isTargetReached = true;

            _animator.SetBool("IsTargetReached", _isTargetReached);
        }
    }

    private void RotateToTargetPosition(Vector3 targetPosition)
    {
        Vector3 lookPoint = targetPosition - transform.position;
        var rotation = Quaternion.LookRotation(lookPoint);
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _moveSpeed * Time.deltaTime);
    }

    private void ApplyNewTarget(Vector3 targetPosition, Enemy enemyTarget)
    {
        Vector3 targetPositionSpread = new Vector3(Random.insideUnitCircle.x * 1, 0, Random.insideUnitCircle.y * 1);
        targetPosition += targetPositionSpread;
        targetPosition.y = transform.position.y;

        _targetPosition = targetPosition;
        EnemyTarget = enemyTarget;

        _isTargetReached = false;
    }
}
