using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelector : MonoBehaviour
{
    private List<RecruitHouse> _recruitControlPanels = new List<RecruitHouse>();
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryOpenBuilding();
        if (Input.GetKeyDown(KeyCode.Escape) && _recruitControlPanels != null)
            foreach (var panel in _recruitControlPanels)
            {
                panel.ControlPanel.SetActive(false);
            }
    }

    private void TryOpenBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out RecruitHouse recruitHouse))
            {
                recruitHouse.ControlPanel.SetActive(true);
                if (!_recruitControlPanels.Contains(recruitHouse))
                    _recruitControlPanels.Add(recruitHouse);
            }
            //else if (_recruitControlPanels.Count > 0)
            //    foreach (var panel in _recruitControlPanels)
            //    {
            //        panel.ControlPanel.SetActive(false);
            //    }
        }
    }
}
