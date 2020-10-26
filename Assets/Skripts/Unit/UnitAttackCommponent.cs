using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit), typeof(Rigidbody))]
public class UnitAttackCommponent : MonoBehaviour
{
    private Unit _unit;
    private float _attackRange;
    private float _delayBetweenAttacks;
    private float _timeFromLastAttack;

    private Animator _animator;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _attackRange = _unit.AttackRange;
        _delayBetweenAttacks = _unit.DelayBetweenAttacks;
        _timeFromLastAttack = _delayBetweenAttacks;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_unit.IsAlive)
        {
            if (_unit.GetComponent<UnitMover>().EnemyTarget != null)
            {
                Enemy enemy = _unit.GetComponent<UnitMover>().EnemyTarget;
                var heading = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z) - transform.position;
                if (heading.sqrMagnitude < _attackRange * _attackRange)
                {
                    _animator.SetBool("IsTargetReached", true);

                    if (_timeFromLastAttack >= _delayBetweenAttacks)
                    {
                        _animator.SetTrigger("Attack1");

                        enemy.ApplyDamage(_unit);
                        _timeFromLastAttack = 0;
                    }
                }
            }
            _timeFromLastAttack += Time.deltaTime;
        }
    }
}
