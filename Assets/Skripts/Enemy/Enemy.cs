using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _delayBetweenAttacks;

    [SerializeField] private GameObject _dieFX;

    public float Damage => _damage;
    public float AttackRange => _attackRange;
    public float DelayBetweenAttacks => _delayBetweenAttacks;


    public void ApplyDamage(Unit unit)
    {
        _health -= unit.Damage;
        if (_health <= 0)
        {
            GameObject fx = Instantiate(_dieFX, transform.position, Quaternion.identity);
            Destroy(fx, 3);
            Destroy(gameObject);
        }
    }
}
