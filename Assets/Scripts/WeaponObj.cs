﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponObj : ScriptableObject
{
    [SerializeField] GameObject weaponModel;
    [SerializeField] AnimatorOverrideController animOverrideControl;

}
