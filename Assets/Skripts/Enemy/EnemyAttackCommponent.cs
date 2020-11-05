using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAttackCommponent : MonoBehaviour
{
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private Fireball _fireball;
    [SerializeField] private Transform _shootPoint;

    private Enemy _enemy;
    private float _attackRange;
    private float _delayBetweenAttacks;
    private float _timeFromLastAttack;

    private List<Unit> _unitsInAttackRange = new List<Unit>();

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _attackRange = _enemy.AttackRange;
        _delayBetweenAttacks = _enemy.DelayBetweenAttacks;
        _timeFromLastAttack = _delayBetweenAttacks;

        _sphereCollider.radius = _attackRange;
    }

    private void Update()
    {
        if (_unitsInAttackRange.Count > 0 && _timeFromLastAttack >= _delayBetweenAttacks)
        {
            for (int i = 0; i < _unitsInAttackRange.Count; i++)
            {
                if (_unitsInAttackRange[i] == null)
                {
                    _unitsInAttackRange.RemoveAt(i);
                    i--;
                }

                else
                {
                    if (_unitsInAttackRange[i].IsAlive == true)
                    {
                        _unitsInAttackRange[i].ApplyDamage(_enemy);
                        _timeFromLastAttack = 0;

                        Fireball fireball = Instantiate(_fireball, _shootPoint.position, Quaternion.identity);
                        fireball.ApplyTarget(_unitsInAttackRange[i]);

                    }
                    return;
                }
            }
        }   
        _timeFromLastAttack += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            _unitsInAttackRange.Add(unit);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            _unitsInAttackRange.Remove(unit);
        }
    }
}
