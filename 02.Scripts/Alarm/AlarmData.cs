using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlarmData", menuName = "ScriptableObjects/AlarmData", order = 1)]
public class AlarmData : ScriptableObject
{
    public bool isAM; //12시간제의 경우, true일시 오전으로 처리
    public List<int> alarmTime = new() { 0, 0 } ; //알람 시간(0 = 시간, 1 = 분)
    public List<bool> isAlarmDay = new() { false, false, false, false, false, false, false }; //요일별 알람 여부

    public DateTime dateTime;

    public string alarmLabel; //알람 라벨
    public bool isActive;     //알람 설정
}