using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class RecruitProgressBar : MonoBehaviour
{
    [SerializeField] private RecruitHouse _recruitHouse;

    private Slider _progressBar;

    private void OnEnable()
    {
        _recruitHouse.ProgressChanged += OnProgressChanged;
    }

    private void OnDisable()
    {
        _recruitHouse.ProgressChanged -= OnProgressChanged;
    }

    private void Start()
    {
        _progressBar = GetComponent<Slider>();
    }

    private void OnProgressChanged(float progress)
    {
        _progressBar.value = progress;
    }
}
