using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitHouseControlPanel : MonoBehaviour
{
    [SerializeField] private List<UnitStats> _units;
    [SerializeField] private Player _player;
    [SerializeField] private UnitView _template;
    [SerializeField] private GameObject _itemContainer;

    private void Start()
    {
        for (int i = 0; i < _units.Count; i++)
        {
            AddItem(_units[i]);
        }
    }

    private void AddItem(UnitStats unit)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.RecruitButtonClick += OnRecruitButtonClick;
        view.Render(unit);
    }

    private void OnRecruitButtonClick(UnitStats unit)
    {
        if (TryRecruitUnit(unit))
        {
            _player.RecruitUnit(unit);
        }
    }

    private bool TryRecruitUnit(UnitStats unit)
    {
        return _player.Money >= unit.Price;
    }
}
