using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlarmData", menuName = "ScriptableObjects/AlarmData", order = 1)]
public class AlarmData : ScriptableObject
{
    public DateTime dateTime = new(2023, 1, 1, 0, 0, 0);

    public bool isAM; //12�ð����� ���, true�Ͻ� �������� ó��

    public int[] alarmTime = { 0, 0 }; //�˶� �ð�(0 = �ð�, 1 = ��)

    public List<bool> isAlarmDay = new()
    { false, false, false, false, false, false, false }; //���Ϻ� �˶� ����

    public string alarmLabel; //�˶� ��

    public bool isActive;     //�˶� ����
}