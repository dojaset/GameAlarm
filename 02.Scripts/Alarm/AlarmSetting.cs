using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class AlarmSetting : MonoBehaviour
{
    AlarmData alarmData; //�˶� ������ ����

    [Header("��ܹ� ��ư")]
    public Button[] topBarButtons; //0 = ���, 1 = ����, 2 = Ȯ��

    [Space]
    [Header("�˶� �ð� ����")]
    public TextMeshProUGUI[] alarmTimeText; //0 = AM/PM, 1 = �ð�, 2 = ��
    public TextMeshProUGUI remainTimeText;  //���� �˶����� ���� �ð� �ؽ�Ʈ
    DateTime alarmDateTime; //ȭ����� DateTime. ���� �����Ϳʹ� ��� X
    TimeSpan remainTime;    //�� ������ ���� �ð��� ������ ����ϱ� ���� ����

    [Space]
    [Header("�ݺ� ����")]
    public Toggle[] repeatToggles; //���� ���
    public Button calanderButton;  //�޷� ��ư

    [Space]
    [Header("�Ҹ� �� ���� ����")]
    public Toggle[] soundToggles; //0 = �Ҹ�, 1 = ����
    public TextMeshProUGUI soundNameText; //�˶��� �̸�

    [Space]
    [Header("�˶� ���� / �̷��")]
    public TextMeshProUGUI intervalText; //���� �� ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI delayText;    //�̷�� �� ǥ�� �ؽ�Ʈ
    int intervalMin;  int intervalNum;   //���� ��, Ƚ��
    int delayMin;     int delayNum;      //�̷�� ��, Ƚ��

    [Space]
    [Header("�� ��")]
    public Scrollbar scrollbar;          //ȭ�� ��ü ��ũ�ѹ�
    public TextMeshProUGUI labelText;    //�� �ؽ�Ʈ
    public TextMeshProUGUI gameNameText; //���� ���� �ؽ�Ʈ
    public Toggle holidayToggle;         //�ް� ��� ���

    void Awake()
    {
        if (GameManager.Ins.alarmData) { alarmData = GameManager.Ins.alarmData; }
        else { alarmData = ScriptableObject.CreateInstance<AlarmData>(); }
        //�˶� ����: �Ŵ����κ��� ������ ������ �ǳ׹���
        //�ű� ����: �ӽ� ������ ���� ����
    }

    void Start() 
    {
        scrollbar.value = 1.0f; //��ũ�ѹ� �� �ʱ�ȭ

        if (!GameManager.Ins.is24Hour) { alarmTimeText[0].gameObject.SetActive(false); }
        else { alarmTimeText[0].text = alarmData.meridiem; }
        //AM, PM ǥ�� (24�ð��� ���� �� ������Ʈ �̻��)

        alarmTimeText[1].text = alarmData.alarmHour.ToString();
        alarmTimeText[2].text = alarmData.alarmMinute.ToString();
        //�˶� �ð�, �� ǥ�� -> ���� InputField������ �� �ȵ�

        for (int i = 0; i < repeatToggles.Length; i++) { repeatToggles[i].isOn = alarmData.repeatDay[i]; }
        //���Ϻ� �ݺ� ���� ǥ��

        if (alarmData.alarmLabel != null)
        {
            labelText.text = alarmData.alarmLabel;
            labelText.color = new(0, 0, 0, 255);
            //�� �ؽ�Ʈ ǥ�� �� �÷� ����(��)
        }
        else
        {
            labelText.text = "�˶� ȭ�鿡 ǥ��";
            labelText.color = new Color(0, 0, 0, 150);
            //�� ���� ǥ�� �� �÷� ����(�׷���)
        }

        if (alarmData.audioClip) //�˶��� ���� ��
        {
            soundNameText.text = alarmData.audioClip.name;
            soundToggles[0].isOn = alarmData.soundMode;
            //�˶��� �̸� �ؽ�Ʈ, Ȱ�� ���� ǥ��
        }
        else
        {
            soundNameText.text = "����";
            soundToggles[0].isOn = false;
            //�̼��ý� ���� ó�� �� ��� off
        }

        soundToggles[1].isOn = alarmData.vibrationMode; //���� Ȱ�� ���� ǥ��

        gameNameText.text = alarmData.gameType; //���� �̸� �ؽ�Ʈ ǥ��

        intervalMin = alarmData.intervalMin;
        intervalNum = alarmData.intervalNum;
        //�ݺ� ������ ����

        delayMin = alarmData.delayMin;
        delayNum = alarmData.delayNum;
        //�̷�� ������ ����

        if (intervalMin == 0 || intervalNum == 0) { intervalText.text = "������� ����"; }
        else { intervalText.text = string.Format("{0}�� �������� {1}��", intervalMin, intervalNum); }
        //�ݺ� ������ �ؽ�Ʈ ǥ��

        if (delayMin == 0 || delayNum == 0) { delayText.text = "������� ����"; }
        else { delayText.text = string.Format("{0}�� �������� {1}��", delayMin, delayNum); }
        //�̷�� ������ �ؽ�Ʈ ǥ��

        holidayToggle.isOn = alarmData.holidayMode;
        //�ް���� Ȱ�� ���� ǥ��
    }

    void Update()
    {
        GameManager.Ins.ShowRemainTime(alarmDateTime, remainTimeText);
    }

    public void SaveAlarmSetting() //���� ������ AlarmData�� �ݿ�
    {
        //alarmData.alarmHour = int.Parse(alarmTimeText[1].text);
        //alarmData.alarmMinute = int.Parse(alarmTimeText[2].text);
        //�˶� �ð� ������ �ݿ� -> InputField ������ �ȵ�
        
        alarmData.alarmDateTime = new DateTime(2023, 4, 5, alarmData.alarmHour, alarmData.alarmMinute, 0);
        //DateTime �ӽ� ����

        for (int i = 0; i < repeatToggles.Length; i++)
        {
            alarmData.repeatDay[i] = repeatToggles[i].isOn;
            //���Ϻ� �ݺ� ������ �ݿ�
        }

        alarmData.alarmLabel = labelText.text;
        //�� �ݿ�

        //alarmData.audioClip
        alarmData.soundMode = soundToggles[0].isOn;
        alarmData.vibrationMode = soundToggles[1].isOn;
        //�Ҹ� �� ���� ������ �ݿ�

        alarmData.gameType = gameNameText.text;
        //���� ���� �ݿ�

        alarmData.intervalMin = intervalMin;
        alarmData.intervalNum = intervalNum;
        //���� ������ �ݿ�

        alarmData.delayMin = delayMin;
        alarmData.delayNum = delayNum;
        //�̷�� ������ �ݿ�

        alarmData.holidayMode = holidayToggle.isOn;
        //�ް���� ������ �ݿ�

        alarmData.updateDate = DateTime.Now.ToString("yy-MM-dd HH mm ss");
        //������ �������� �ݿ�

        if (alarmData.createDate == null) { CreateAlarmDataFile(); }
    }

    void CreateAlarmDataFile()
    {
        alarmData.createDate = DateTime.Now.ToString("yy-MM-dd HH mm ss");
        //�������� ����

        string filename = "Assets/UserDatas/" + alarmData.createDate + ".asset";
        //���� ���, �̸� ��� ����

        AssetDatabase.CreateAsset(alarmData, filename);
        AssetDatabase.SaveAssets();
        //���� ����, ���� ��Ŀ� ���� ����

        GameManager.Ins.alarmData = alarmData;
        //�Ŵ��� ������Ʈ�� ������ ���� ����

        GameManager.Ins.isNewAlarm = true;
        //�� ������ ������ �ʿ��� ���·� ��ȯ
    }
}