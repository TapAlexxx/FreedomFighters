using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UnitView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _recruitButton;

    private UnitStats _unitStats;

    public event UnityAction<UnitStats> RecruitButtonClick;

    private void OnEnable()
    {
        _recruitButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _recruitButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(UnitStats unitStats)
    {
        _unitStats = unitStats;

        _label.text = unitStats.Label;
        _price.text = unitStats.Price.ToString();
        _icon.sprite = unitStats.Icon;
    }

    private void OnButtonClick()
    {
        RecruitButtonClick?.Invoke(_unitStats);
    }
}
