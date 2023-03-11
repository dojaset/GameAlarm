using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AlarmManager : MonoBehaviour
{
    public Scrollbar scrollbar;         //��ũ�ѹ�
    public TextMeshProUGUI dateTimeNow; //���� �ð� ǥ��

    public TextMeshProUGUI nextAlarm;   //���� �˶����� ���� �ð� ǥ��
    public Transform alarmList;         //�˶��� �����Ǵ� �θ� ������Ʈ
    public List<AlarmData> alarmDatas;  //�� �˶� ���� ������ ����Ʈ

    public bool isHour24 = false;       //24�ð����� �����ƴ��� �Ǵ��ϴ� ����(�ӽ�)

    void Start()
    {
        scrollbar.value = 1.0f; //��ũ�ѹ� �� �ʱ�ȭ
        NextAlarm();
    }

    void Update()
    {
        DateTimeNow(); //���� �ð� ǥ��
    }

    void DateTimeNow() //���� �ð� ǥ��
    {
        DateTime now = DateTime.Now;

        if (isHour24) //24�ð����� ������ ���
        {
            dateTimeNow.text = now.ToLongDateString() + "\n" + now.ToString("HH : mm : ss");
            //0000�� 0�� 0�� �Ͽ���
            //   00 : 00 : 00
        }
        else //12�ð����� ������ ���
        {
            dateTimeNow.text = now.ToLongDateString() + "\n" + now.ToString("tt hh : mm : ss");
            //0000�� 0�� 0�� �Ͽ���
            // ���� 00 : 00 : 00
        }
    }

    void NextAlarm() //���� �˶����� ���� �ð� ǥ��
    {
        for (int i = 0; i < alarmList.transform.childCount; i++)
        {
            Transform alarm = alarmList.GetChild(i);

            if (alarm.name == "Add Alarm") { return; }

            alarmDatas.Add(alarm.GetComponent<NewAlarm>().alarmData);
        }
        alarmDatas.TrimExcess();
    }
}
