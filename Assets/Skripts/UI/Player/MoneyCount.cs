using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyCount;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.MoneyCountChanged += OnMoneyCountChanged;
    }

    private void OnDisable()
    {
        _player.MoneyCountChanged -= OnMoneyCountChanged;
    }

    private void OnMoneyCountChanged(int money)
    {
        _moneyCount.text = money.ToString();
    }
}
