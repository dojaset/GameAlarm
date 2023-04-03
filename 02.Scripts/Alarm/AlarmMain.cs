using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlarmMain : MonoBehaviour
{
    public Scrollbar scrollbar;         //스크롤바
    public TextMeshProUGUI dateTimeNow; //현재 시각 표시
    public TextMeshProUGUI nextAlarm;   //다음 알람까지 남은 시간 표시

    public Transform addAlarmBtn;       //알람 생성 버튼 위치
    public GameObject alarmObject;      //개별 알람 프리팹

    public Transform alarmList;         //알람이 생성되는 부모 오브젝트
    public List<AlarmData> alarmDatas;  //각 알람 설정 데이터 리스트

    void Start()
    {
        if (GameManager.Ins.isNewAlarm) { CreateNewAlarmPrefab(); } //필요시 새 프리팹 생성

        GameManager.Ins.alarmData = null; //전달 변수 초기화

        UpdateAlarmList(); //알람 데이터 리스트화

        scrollbar.value = 1.0f; //스크롤바 값 초기화
    }

    void Update()
    {
        DateTimeNow(); //현재 시간 표시
        GameManager.Ins.ShowRemainTime(alarmDatas[0].alarmDateTime, nextAlarm);
    }

    void CreateNewAlarmPrefab()
    {
        GameObject newAlarm = Instantiate(alarmObject, alarmList); //새 개별 알람 오브젝트 생성

        newAlarm.GetComponent<AlarmObject>().alarmData = GameManager.Ins.alarmData; //데이터 전달

        GameManager.Ins.isNewAlarm = false; //프리팹 생성 필요 상태 off

        addAlarmBtn.SetAsLastSibling();     //새 알람 생성 버튼 위치 변경(맨 아래)
    }

    void DateTimeNow() //현재 시간 표시
    {
        if (GameManager.Ins.is24Hour) //24시간제로 설정된 경우
        {
            dateTimeNow.text = DateTime.Now.ToString("yyyy년 M월 d일 dddd\nHH : mm : ss ");
            //0000년 0월 0일 일요일
            //   00 : 00 : 00
        }
        else //12시간제로 설정된 경우
        {
            dateTimeNow.text = DateTime.Now.ToString("yyyy년 M월 d일 dddd\n tt HH : mm : ss ");
            //0000년 0월 0일 일요일
            // 오전 00 : 00 : 00
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
