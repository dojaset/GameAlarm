using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlarmObject : MonoBehaviour
{
    public AlarmData alarmData;  //알람 설정 데이터
    public Toggle toggle;        //알람 활성 상태 토글

    TextMeshProUGUI[] alarmText; //텍스트 배열(0 = 라벨, 1 = 시간, 2 = 요일)

    string[] week = new string[] { "월", "화", "수", "목", "금", "토", "일" };
    string meridiem; //AM, PM

    string color = "B4B4B4";

    void Awake()
    {
        alarmText = GetComponentsInChildren<TextMeshProUGUI>(); //TMP 컴포넌트 불러오기
    }

    void Start()
    {
        name = alarmData.createDate; //오브젝트명을 생성일자로 변경

        alarmText[0].text = alarmData.alarmLabel; //알람 라벨 표시

        StatusUpdate(); //알람 활성 상태 표시
        TimeUpdate();   //알람 설정 시간 표시
        DayUpdate();    //알람 설정 요일 표시
    }

    void Update()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameManager.Ins.alarmData = this.alarmData; //매니저에 데이터 파일 전달
            SceneChanger.GotoScene("AlarmSetting");     //알람 설정 씬으로 이동
        });

        //문제점: 직접 지정 시보다 많이 느림
    }

    void StatusUpdate()
    {
        alarmData.isActive = alarmData.repeatDay.Contains(true);
        //알람이 울리도록 설정된 요일이 있다면 알람 활성 상태를 on

        toggle.isOn = alarmData.isActive; //알람 활성 상태를 토글에 반영
    }

    void TimeUpdate() //시간 텍스트 변경
    {
        if (GameManager.Ins.is24Hour) //24시간제로 설정된 경우
        {
            alarmText[1].text = string.Format("{0:D2} : {1:D2}", alarmData.alarmHour, alarmData.alarmMinute);
            //00 : 00
        }
        else //12시간제로 설정된 경우
        {
            alarmText[1].text = string.Format("{0:D2} : {1:D2} <b><size=60>{2}</size></b>",
                alarmData.alarmHour, alarmData.alarmMinute, meridiem); //00 : 00 오전
        }
    }

    void DayUpdate() //요일 텍스트 변경
    {
        if (!alarmData.repeatDay.Contains(false)) { alarmText[2].text = "매일"; }
        //모든 요일 -> 매일

        else if (alarmData.repeatDay[0] && alarmData.repeatDay[1] && alarmData.repeatDay[2] && alarmData.repeatDay[3]
            && alarmData.repeatDay[4] && !alarmData.repeatDay[5] && !alarmData.repeatDay[6]) { alarmText[2].text = "평일"; }
        //월~금요일 -> 평일

        else if (!alarmData.repeatDay[0] && !alarmData.repeatDay[1] && !alarmData.repeatDay[2] && !alarmData.repeatDay[3]
            && !alarmData.repeatDay[4] && alarmData.repeatDay[5] && alarmData.repeatDay[6]) { alarmText[2].text = "주말"; }
        //토~일요일 -> 주말

        else //그 외
        {
            alarmText[2].text = null; //요일 텍스트 초기화

            for (int i = 0; i < alarmData.repeatDay.Count; i++) //요일 수만큼 반복
            {
                if (alarmData.repeatDay[i]) { alarmText[2].text += " " + week[i]; }
                else { alarmText[2].text += string.Format(" <color=#{0}>{1}</color>", color, week[i]); }
                //알람이 울리도록 설정된 경우 일반 요일 텍스트 추가, 그 외엔 회색 요일 텍스트 추가
                //월 화 수 목 금 토 일
            }
        }
    }
}