using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlarmObject : MonoBehaviour
{
    public AlarmData alarmData;  //�˶� ���� ������
    public Toggle toggle;        //�˶� Ȱ�� ���� ���

    TextMeshProUGUI[] alarmText; //�ؽ�Ʈ �迭(0 = ��, 1 = �ð�, 2 = ����)

    string[] week = new string[] { "��", "ȭ", "��", "��", "��", "��", "��" };
    string meridiem; //AM, PM

    string color = "B4B4B4";

    void Awake()
    {
        alarmText = GetComponentsInChildren<TextMeshProUGUI>(); //TMP ������Ʈ �ҷ�����
    }

    void Start()
    {
        name = alarmData.createDate; //������Ʈ���� �������ڷ� ����

        alarmText[0].text = alarmData.alarmLabel; //�˶� �� ǥ��

        StatusUpdate(); //�˶� Ȱ�� ���� ǥ��
        TimeUpdate();   //�˶� ���� �ð� ǥ��
        DayUpdate();    //�˶� ���� ���� ǥ��
    }

    void Update()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameManager.Ins.alarmData = this.alarmData; //�Ŵ����� ������ ���� ����
            SceneChanger.GotoScene("AlarmSetting");     //�˶� ���� ������ �̵�
        });

        //������: ���� ���� �ú��� ���� ����
    }

    void StatusUpdate()
    {
        alarmData.isActive = alarmData.repeatDay.Contains(true);
        //�˶��� �︮���� ������ ������ �ִٸ� �˶� Ȱ�� ���¸� on

        toggle.isOn = alarmData.isActive; //�˶� Ȱ�� ���¸� ��ۿ� �ݿ�
    }

    void TimeUpdate() //�ð� �ؽ�Ʈ ����
    {
        if (GameManager.Ins.is24Hour) //24�ð����� ������ ���
        {
            alarmText[1].text = string.Format("{0:D2} : {1:D2}", alarmData.alarmHour, alarmData.alarmMinute);
            //00 : 00
        }
        else //12�ð����� ������ ���
        {
            alarmText[1].text = string.Format("{0:D2} : {1:D2} <b><size=60>{2}</size></b>",
                alarmData.alarmHour, alarmData.alarmMinute, meridiem); //00 : 00 ����
        }
    }

    void DayUpdate() //���� �ؽ�Ʈ ����
    {
        if (!alarmData.repeatDay.Contains(false)) { alarmText[2].text = "����"; }
        //��� ���� -> ����

        else if (alarmData.repeatDay[0] && alarmData.repeatDay[1] && alarmData.repeatDay[2] && alarmData.repeatDay[3]
            && alarmData.repeatDay[4] && !alarmData.repeatDay[5] && !alarmData.repeatDay[6]) { alarmText[2].text = "����"; }
        //��~�ݿ��� -> ����

        else if (!alarmData.repeatDay[0] && !alarmData.repeatDay[1] && !alarmData.repeatDay[2] && !alarmData.repeatDay[3]
            && !alarmData.repeatDay[4] && alarmData.repeatDay[5] && alarmData.repeatDay[6]) { alarmText[2].text = "�ָ�"; }
        //��~�Ͽ��� -> �ָ�

        else //�� ��
        {
            alarmText[2].text = null; //���� �ؽ�Ʈ �ʱ�ȭ

            for (int i = 0; i < alarmData.repeatDay.Count; i++) //���� ����ŭ �ݺ�
            {
                if (alarmData.repeatDay[i]) { alarmText[2].text += " " + week[i]; }
                else { alarmText[2].text += string.Format(" <color=#{0}>{1}</color>", color, week[i]); }
                //�˶��� �︮���� ������ ��� �Ϲ� ���� �ؽ�Ʈ �߰�, �� �ܿ� ȸ�� ���� �ؽ�Ʈ �߰�
                //�� ȭ �� �� �� �� ��
            }
        }
    }
}