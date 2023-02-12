using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlarmManager : MonoBehaviour
{
    public Scrollbar scrollbar;         //��ũ�ѹ�
    public TextMeshProUGUI dateTimeNow; //���� �ð� ǥ��

    public TextMeshProUGUI nextAlarm;   //���� �˶����� ���� �ð� ǥ��
    public GameObject alarmList;        //�˶��� �����Ǵ� �θ� ������Ʈ
    public List<NewAlarm> alarmDatas;   //�� �˶� ���� ������ ����Ʈ

    public bool isHour24 = false;       //24�ð����� �����ƴ��� �Ǵ��ϴ� ����(�ӽ�)

    void Start()
    {
        scrollbar.value = 1.0f; //��ũ�ѹ� �� �ʱ�ȭ
    }

    void Update()
    {
        DateTimeNow(); //���� �ð� ǥ��
    }

    void DateTimeNow() //���� �ð� ǥ��
    {
        if (isHour24) //24�ð����� ������ ���
        {
            dateTimeNow.text = DateTime.Now.ToLongDateString() + "\n" + DateTime.Now.ToString("HH : mm : ss");
            //0000�� 0�� 0�� �Ͽ���
            //   00 : 00 : 00
        }
        else //12�ð����� ������ ���
        {
            dateTimeNow.text = DateTime.Now.ToLongDateString() + "\n" + DateTime.Now.ToString("tt hh : mm : ss");
            //0000�� 0�� 0�� �Ͽ���
            // ���� 00 : 00 : 00
        }
    }

    void NextAlarm() //���� �˶����� ���� �ð� ǥ��
    {
        for (int i = 0; i < alarmList.transform.childCount; i++)
        {
            alarmDatas.Add(alarmList.transform.GetChild(i).GetComponent<NewAlarm>());
        }
        alarmDatas.TrimExcess();
    }
}
