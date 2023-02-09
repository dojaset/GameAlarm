using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlarmManager : MonoBehaviour
{
    public Scrollbar scrollbar;         //스크롤바
    public TextMeshProUGUI dateTimeNow; //현재 시각 표시

    public TextMeshProUGUI nextAlarm;   //다음 알람까지 남은 시간 표시
    public GameObject alarmList;        //알람이 생성되는 부모 오브젝트
    public List<NewAlarm> alarmDatas;   //각 알람 설정 데이터 리스트

    public bool isHour24 = false;       //24시간제로 설정됐는지 판단하는 변수(임시)

    void Start()
    {
        scrollbar.value = 1.0f; //스크롤바 값 초기화
    }

    void Update()
    {
        if (isHour24) //24시간제로 설정된 경우
        {
            dateTimeNow.text = DateTime.Now.ToLongDateString() + "\n" + DateTime.Now.ToString("HH : mm : ss");
        }
        else //12시간제로 설정된 경우
        {
            dateTimeNow.text = DateTime.Now.ToLongDateString() + "\n" + DateTime.Now.ToString("tt hh : mm : ss");
        }
    }

    void NextAlarm() //다음 알람까지 남은 시간 표시
    {
        for (int i = 0; i < alarmList.transform.childCount; i++)
        {
            alarmDatas.Add(alarmList.transform.GetChild(i).GetComponent<NewAlarm>());
        }
        alarmDatas.TrimExcess();
    }
}
