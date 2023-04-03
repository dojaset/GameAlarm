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
        TimeSpan remainTime = alarmTime - DateTime.Now; //ȭ�� �� ���� �ð��� ���� �ð��� ���� ���

        if (remainTime.Minutes > 0)  //0�� �̻�
        {
            if (remainTime.Days > 0) //�Ϸ� �̻�
            {
                remainText.text = string.Format("���� �˶����� {0}�� {1}�ð� {2}��",
                    remainTime.Days, remainTime.Hours, remainTime.Minutes);
            }
            else
            {
                remainText.text = string.Format("���� �˶����� {0}�ð� {1}��",
                    remainTime.Hours, remainTime.Minutes);
            }
        }
    }
}