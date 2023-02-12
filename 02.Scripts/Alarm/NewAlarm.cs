using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewAlarm : MonoBehaviour
{
    public AlarmData alarmData;  //알람 설정 데이터
    public Toggle toggle;        //알람 설정 상태 토글

    TextMeshProUGUI[] alarmText; //텍스트 배열(0 = 시간, 1 = 요일, 2 = 라벨)

    string[] alarmDay = new string[] { "월", "화", "수", "목", "금", "토", "일" };
    string meridiem; //오전 or 오후

    bool isHour24 = false; //24시간 표시 방식인지를 판단하는 변수(임시)

    void Awake()
    {
        alarmText = GetComponentsInChildren<TextMeshProUGUI>(); //TMP 컴포넌트 불러오기

        alarmText[2].text = alarmData.alarmLabel; //알람 라벨을 데이터 파일의 라벨로 변경

        if (!alarmData.isAlarmDay.Contains(true)) { alarmData.isActive = false; }
        //알람이 울리도록 설정된 요일이 없다면 알람 활성 상태를 off

        toggle.isOn = alarmData.isActive;

        TimeUpdate(); //알람 설정 시간 표시
        DayUpdate();  //알람 설정 요일 표시
    }

    void TimeUpdate()
    {
        if (isHour24) //24시간제로 설정된 경우
        {
            alarmText[0].text = string.Format("{0:D2} : {1:D2}", alarmData.alarmTime[0], alarmData.alarmTime[1]);
            //00 : 00
        }
        else //12시간제로 설정된 경우
        {
            if (alarmData.isAM) { meridiem = "AM"; }
            else { meridiem = "PM"; }

            alarmText[0].text = string.Format("{0:D2} : {1:D2} <b><size=60>{2}</size></b>",
                alarmData.alarmTime[0], alarmData.alarmTime[1], meridiem);
            //00 : 00 오전
        }
    }

    void DayUpdate()
    {
        if (!alarmData.isAlarmDay.Contains(false)) { alarmText[1].text = "<color=#429AFF>매일</color>"; }
        //모든 요일에 알람이 울리도록 설정된 경우 요일 텍스트를 '매일'로 변경

        else if (alarmData.isAlarmDay[0] && alarmData.isAlarmDay[1] && alarmData.isAlarmDay[2]
            && alarmData.isAlarmDay[3] && alarmData.isAlarmDay[4]) { alarmText[1].text = "<color=#429AFF>평일</color>"; }
        //월~금요일에 알람이 울리도록 설정된 경우 요일 텍스트를 '평일'로 변경

        else if (alarmData.isAlarmDay[5] && alarmData.isAlarmDay[6]) { alarmText[1].text = "<color=#429AFF>주말</color>"; }
        //토~일요일에 알람이 울리도록 설정된 경우 요일 텍스트를 '주말'로 변경

        else
        {
            alarmText[1].text = null; //요일 텍스트 초기화

            for (int i = 0; i < alarmData.isAlarmDay.Count; i++) //요일 수만큼 반복
            {
                if (alarmData.isAlarmDay[i]) { alarmText[1].text += " " + alarmDay[i]; }
                else { alarmText[1].text += string.Format(" <color=#B4B4B4>{0}</color>", alarmDay[i]); }
                //알람이 울리도록 설정된 경우 일반 요일 텍스트 추가, 그 외엔 회색 요일 텍스트 추가
                //월 화 수 목 금 토 일
            }
        }
    }
}