using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RecruitHouse : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _controlPanel;
    [SerializeField] private UnitStats[] _unit;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _gatheringPoint;
    [SerializeField] private float _recruitTime;

    public event UnityAction<float> ProgressChanged;
    public event UnityAction<int> UnitsToRecruitCountChanged;

    public GameObject ControlPanel => _controlPanel;

    private Queue<Unit> _unitsToRecruit = new Queue<Unit>();
    private float _timeFromLastRecruit;
    private float _normalizedRecruitingProgress;

    private void OnEnable()
    {
        _player.UnitRecruited += OnUnitRecruited;
    }

    private void OnDisable()
    {
        _player.UnitRecruited -= OnUnitRecruited;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _controlPanel.activeSelf == true)
        {
            _player.RecruitUnit(_unit[0]);
        }

        if (Input.GetKeyDown(KeyCode.R) && _controlPanel.activeSelf == true)
        {
            _player.RecruitUnit(_unit[1]);
        }

        if (_timeFromLastRecruit >= _recruitTime && _unitsToRecruit.Count > 0)
        {
            SpawnUnit(_unitsToRecruit.Dequeue());
            _timeFromLastRecruit = 0;

            UnitsToRecruitCountChanged?.Invoke(_unitsToRecruit.Count);
        }

        if (_timeFromLastRecruit <= _recruitTime)
        {
            _normalizedRecruitingProgress = _timeFromLastRecruit / _recruitTime;
            ProgressChanged?.Invoke(_normalizedRecruitingProgress);
        }

        _timeFromLastRecruit += Time.deltaTime;
    }


    private void OnUnitRecruited(GameObject unit)
    {
        AddInQueue(unit);
    }

    private void AddInQueue(GameObject unit)
    {
        if (_unitsToRecruit.Count < 1)
        {
            _timeFromLastRecruit = 0;
        }

        Unit recruitedUnit = unit.GetComponent<Unit>();
        _unitsToRecruit.Enqueue(recruitedUnit);
        UnitsToRecruitCountChanged?.Invoke(_unitsToRecruit.Count);
    }

    private void SpawnUnit(Unit unit)
    {
        Unit spawnedUnit = Instantiate(unit);
        spawnedUnit.transform.position = _spawnPoint.position;
        spawnedUnit.ApplyNewTarget(_gatheringPoint.position);
    }
}
