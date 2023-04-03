using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "AlarmData", menuName = "ScriptableObjects/AlarmData", order = 1)]
public class AlarmData : ScriptableObject
{
    [Header("알람 시각")]
    [Range(0, 23)] public int alarmHour;
    [Range(0, 59)] public int alarmMinute;
    public string meridiem;

    [Space]
    [Header("반복 설정")]
    public List<bool> repeatDay = new()
    { false, false, false, false, false, false, false }; //요일별 알람 여부
    public DateTime alarmDateTime;

    [Space]
    [Header("소리 및 진동")]
    public AudioClip audioClip; //알람음
    public bool soundMode;      //소리 모드(중복 가능)
    public bool vibrationMode;  //진동 모드(중복 가능)

    [Space]
    [Header("알람 간격")]
    public int intervalMin; //재알람 시간(분 단위)
    public int intervalNum; //재알람 횟수

    [Space]
    [Header("알람 미루기")]
    public int delayMin;    //미룰 시간(분 단위)
    public int delayNum;    //미룰 횟수

    [Space]
    [Header("그 외 설정")]
    public string alarmLabel; //알람 라벨
    public string gameType;   //게임 타입
    public bool holidayMode;  //휴가 모드
    public bool isActive;     //알람 활성 상태

    [Space]
    [Header("생성 및 변경 일자")]
    public string createDate; 
    public string updateDate;
}