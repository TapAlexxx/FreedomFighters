using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _money;

    public event UnityAction<GameObject> UnitRecruited;
    public event UnityAction<int> MoneyCountChanged;

    public int Money => _money;

    private void Awake()
    {
        MoneyCountChanged?.Invoke(_money);
    }

    public void TryRecruitUnit(UnitStats unit)
    {
        if (_money >= unit.Price)
        {
            _money -= unit.Price;
            MoneyCountChanged?.Invoke(_money);
            UnitRecruited?.Invoke(unit.Prefab);
        }
    }

    public void CollectMoney(int money)
    {
        _money += money;
        MoneyCountChanged?.Invoke(_money);
    }
}
