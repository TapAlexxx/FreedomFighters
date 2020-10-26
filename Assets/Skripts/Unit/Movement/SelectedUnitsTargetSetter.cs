using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectedUnitsTargetSetter : MonoBehaviour
{
    [SerializeField] private UnitSelector _unitSelector;
    [SerializeField] private GameObject _targetPointerFX;

    private List<Unit> _selectedUnits;
    private Enemy _enemyTarget;
    private Vector3 _targetPosition;

    private void OnEnable()
    {
        _unitSelector.UnitsSelected += SetSelectedUnits;
    }

    private void OnDisable()
    {
        _unitSelector.UnitsSelected -= SetSelectedUnits;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && _selectedUnits != null)
            foreach (var unit in _selectedUnits)
            {
                if (TrySetNewTargetPositionForUnit())
                    unit.ApplyNewTarget(_targetPosition, _enemyTarget);
            }
    }


    private void SetSelectedUnits(List<Unit> selectedUnits)
    {
        _selectedUnits = selectedUnits;
    }

    private bool TrySetNewTargetPositionForUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        int layerMask = ~LayerMask.GetMask(LayerMask.LayerToName(9));
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject.tag == "Ground")
            {
                GameObject fx = Instantiate(_targetPointerFX, hit.point, Quaternion.identity);
                Destroy(fx, 1);
                if (_selectedUnits.Count > 1)
                {

                    Vector3 targetPositionSpread = UnityEngine.Random.insideUnitSphere * 6;
                    targetPositionSpread.y = 0;
                    _targetPosition = hit.point + targetPositionSpread;
                }
                else
                {
                    _targetPosition = hit.point;
                }
                _enemyTarget = null;
                return true;
            }
            else if (hit.collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                _enemyTarget = enemy;
                _targetPosition = enemy.transform.position;
                return true;
            }
            else return false;
        }
        else
            return false;
    }
}
