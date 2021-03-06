﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;

    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
    public GameObject Prefab => _prefab;
}
