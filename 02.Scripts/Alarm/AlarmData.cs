using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlarmData", menuName = "ScriptableObjects/AlarmData", order = 1)]
public class AlarmData : ScriptableObject
{
    public List<int> alarmTime = new();
    public List<bool> isAlarmDay = new();
    public bool isAM;
    public string alarmLabel;
    public bool isActive;
}
