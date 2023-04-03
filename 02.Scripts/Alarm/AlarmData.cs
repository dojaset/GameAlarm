using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "AlarmData", menuName = "ScriptableObjects/AlarmData", order = 1)]
public class AlarmData : ScriptableObject
{
    [Header("�˶� �ð�")]
    [Range(0, 23)] public int alarmHour;
    [Range(0, 59)] public int alarmMinute;
    public string meridiem;

    [Space]
    [Header("�ݺ� ����")]
    public List<bool> repeatDay = new()
    { false, false, false, false, false, false, false }; //���Ϻ� �˶� ����
    public DateTime alarmDateTime;

    [Space]
    [Header("�Ҹ� �� ����")]
    public AudioClip audioClip; //�˶���
    public bool soundMode;      //�Ҹ� ���(�ߺ� ����)
    public bool vibrationMode;  //���� ���(�ߺ� ����)

    [Space]
    [Header("�˶� ����")]
    public int intervalMin; //��˶� �ð�(�� ����)
    public int intervalNum; //��˶� Ƚ��

    [Space]
    [Header("�˶� �̷��")]
    public int delayMin;    //�̷� �ð�(�� ����)
    public int delayNum;    //�̷� Ƚ��

    [Space]
    [Header("�� �� ����")]
    public string alarmLabel; //�˶� ��
    public string gameType;   //���� Ÿ��
    public bool holidayMode;  //�ް� ���
    public bool isActive;     //�˶� Ȱ�� ����

    [Space]
    [Header("���� �� ���� ����")]
    public string createDate; 
    public string updateDate;
}