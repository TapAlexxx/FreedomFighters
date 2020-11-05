using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UnitMover),typeof(UnitAttackCommponent))]
public class Unit : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _damage;
    [SerializeField] private float _delayBetweenAttacks;

    public GameObject OnSelectCircle;

    public event UnityAction<Vector3, Enemy> TargetChanged;

    public float MoveSpeed => _moveSpeed;
    public float AttackRange => _attackRange;
    public float Damage => _damage;
    public float DelayBetweenAttacks => _delayBetweenAttacks;
    public bool IsAlive { get; private set; }


    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        IsAlive = true;
    }

    public void ApplyNewTarget(Vector3 targetPosition, Enemy targetEnemy =null)
    {
        TargetChanged?.Invoke(targetPosition, targetEnemy);
    }

    public void ApplyDamage(Enemy enemy)
    {
        _health -= enemy.Damage;

        if (_health <= 0)
        {
            IsAlive = false;
            _animator.SetTrigger("Death");
            Destroy(gameObject, 3);
        }
    }
}
