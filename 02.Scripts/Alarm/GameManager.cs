using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singletone<GameManager>
{
    public bool is24Hour;
    public AlarmData alarmData;
    public bool isNewAlarm;

    public void ShowRemainTime(DateTime alarmTime, TextMeshProUGUI remainText)
    {
        TimeSpan remainTime = alarmTime - DateTime.Now; //화면 상 설정 시간과 현재 시간의 차이 계산

        if (remainTime.Minutes > 0)  //0분 이상
        {
            if (remainTime.Days > 0) //하루 이상
            {
                remainText.text = string.Format("다음 알람까지 {0}일 {1}시간 {2}분",
                    remainTime.Days, remainTime.Hours, remainTime.Minutes);
            }
            else
            {
                remainText.text = string.Format("다음 알람까지 {0}시간 {1}분",
                    remainTime.Hours, remainTime.Minutes);
            }
        }
    }
}