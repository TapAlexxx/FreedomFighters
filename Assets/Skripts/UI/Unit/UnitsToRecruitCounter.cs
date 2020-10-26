using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitsToRecruitCounter : MonoBehaviour
{
    [SerializeField] private RecruitHouse _recruitHouse;

    private TMP_Text _unitsCountText;

    private void OnEnable()
    {
        _recruitHouse.UnitsToRecruitCountChanged += OnCountChanged;
    }

    private void OnDisable()
    {
        _recruitHouse.UnitsToRecruitCountChanged -= OnCountChanged;
    }

    private void Start()
    {
        _unitsCountText = GetComponent<TMP_Text>();
    }

    private void OnCountChanged(int count)
    {
        _unitsCountText.text = count.ToString();
    }
}
