using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyFactory : MonoBehaviour
{
    [SerializeField] private int _moneyPerSecond;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _earnedMoneyFX;
    [SerializeField] private Transform _fxSpawnPoint;

    private IEnumerator produceMoney;

    private void Start()
    {
        produceMoney = ProduceMoney(_moneyPerSecond);
        StartCoroutine(produceMoney);
    }

    private IEnumerator ProduceMoney(int moneyPerSecond)
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            _player.CollectMoney(moneyPerSecond);
            
            GameObject fx = Instantiate(_earnedMoneyFX, _fxSpawnPoint.position, Quaternion.identity);
            Destroy(fx, 3);
        }
    }
}
