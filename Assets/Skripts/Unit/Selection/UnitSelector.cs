using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitSelector : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public event UnityAction<List<Unit>> UnitsSelected;

    private List<Unit> _selectedUnits = new List<Unit>();
    private bool _isOnSelecting;
    private Vector3 _startMousePosition;


    public bool IsWithinSelectionBounds(Unit unit)
    {
        if (_isOnSelecting == false)
        {
            return false;
        }

        else
        {
            var viewportBound = RectUtils.GetViewportBounds(_camera, _startMousePosition, Input.mousePosition);
            return viewportBound.Contains(_camera.WorldToViewportPoint(unit.transform.position));
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isOnSelecting = true;
            _startMousePosition = Input.mousePosition;
        }

        if (_isOnSelecting)
        {
            DisablePreviousSelection();
            SetCirclesOnHighlightedUnits();
        }

        if (Input.GetMouseButtonUp(0))
        {
            SetNewSelectedUnits();
        }
    }

    private void OnGUI()
    {
        if (_isOnSelecting)
        {
            var userRect = RectUtils.GetScreenRect(_startMousePosition, Input.mousePosition);
            RectUtils.DrawScreenRect(userRect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            RectUtils.DrawScreenRectBorder(userRect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    private void SetNewSelectedUnits()
    {
        _selectedUnits.Clear();

        foreach (var selectableUnit in FindObjectsOfType<Unit>())
        {
            if (IsWithinSelectionBounds(selectableUnit))
            {
                _selectedUnits.Add(selectableUnit);
            }

            else
            {
                continue;
            }
        }

        UnitsSelected?.Invoke(_selectedUnits);
        _isOnSelecting = false;
    }

    private void DisablePreviousSelection()
    {
        foreach (var unit in FindObjectsOfType<Unit>())
        {
            if (unit.OnSelectCircle.activeSelf == true)
            {
                unit.OnSelectCircle.SetActive(false);
            }
        }
    }

    private void SetCirclesOnHighlightedUnits()
    {
        foreach (var unit in FindObjectsOfType<Unit>())
        {
            if (IsWithinSelectionBounds(unit))
            {
                unit.OnSelectCircle.SetActive(true);
            }

            else
            {
                unit.OnSelectCircle.SetActive(false);
            }
        }
    }
}
