using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlarmMain : MonoBehaviour
{
    public Scrollbar scrollbar;         //��ũ�ѹ�
    public TextMeshProUGUI dateTimeNow; //���� �ð� ǥ��
    public TextMeshProUGUI nextAlarm;   //���� �˶����� ���� �ð� ǥ��

    public Transform addAlarmBtn;       //�˶� ���� ��ư ��ġ
    public GameObject alarmObject;      //���� �˶� ������

    public Transform alarmList;         //�˶��� �����Ǵ� �θ� ������Ʈ
    public List<AlarmData> alarmDatas;  //�� �˶� ���� ������ ����Ʈ

    void Start()
    {
        if (GameManager.Ins.isNewAlarm) { CreateNewAlarmPrefab(); } //�ʿ�� �� ������ ����

        GameManager.Ins.alarmData = null; //���� ���� �ʱ�ȭ

        UpdateAlarmList(); //�˶� ������ ����Ʈȭ

        scrollbar.value = 1.0f; //��ũ�ѹ� �� �ʱ�ȭ
    }

    void Update()
    {
        DateTimeNow(); //���� �ð� ǥ��
        GameManager.Ins.ShowRemainTime(alarmDatas[0].alarmDateTime, nextAlarm);
    }

    void CreateNewAlarmPrefab()
    {
        GameObject newAlarm = Instantiate(alarmObject, alarmList); //�� ���� �˶� ������Ʈ ����

        newAlarm.GetComponent<AlarmObject>().alarmData = GameManager.Ins.alarmData; //������ ����

        GameManager.Ins.isNewAlarm = false; //������ ���� �ʿ� ���� off

        addAlarmBtn.SetAsLastSibling();     //�� �˶� ���� ��ư ��ġ ����(�� �Ʒ�)
    }

    void DateTimeNow() //���� �ð� ǥ��
    {
        if (GameManager.Ins.is24Hour) //24�ð����� ������ ���
        {
            dateTimeNow.text = DateTime.Now.ToString("yyyy�� M�� d�� dddd\nHH : mm : ss ");
            //0000�� 0�� 0�� �Ͽ���
            //   00 : 00 : 00
        }
        else //12�ð����� ������ ���
        {
            dateTimeNow.text = DateTime.Now.ToString("yyyy�� M�� d�� dddd\n tt HH : mm : ss ");
            //0000�� 0�� 0�� �Ͽ���
            // ���� 00 : 00 : 00
        }
    }

    void UpdateAlarmList()
    {
        for (int i = 0; i < alarmList.transform.childCount; i++)
        {
            Transform alarm = alarmList.GetChild(i);

            if (alarm.name == "Add Alarm") { return; }

            alarmDatas.Add(alarm.GetComponent<AlarmObject>().alarmData);
        }
        alarmDatas.TrimExcess();
    }
}
