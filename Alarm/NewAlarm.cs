using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewAlarm : MonoBehaviour
{
    public AlarmData alarmData;  //�˶� ���� ������

    TextMeshProUGUI[] alarmText; //�ؽ�Ʈ �迭

    string[] alarmDay = new string[] { "��", "ȭ", "��", "��", "��", "��", "��" };
    string meridiem; //���� or ����

    bool isHour24 = false; //24�ð� ǥ�� ��������� �Ǵ��ϴ� ����(�ӽ�)

    void Awake()
    {
        alarmText = GetComponentsInChildren<TextMeshProUGUI>();

        alarmText[2].text = alarmData.alarmLabel;

        if (!alarmData.isAlarmDay.Contains(true)) { alarmData.isActive = false; }

        TimeUpdate();
        DayUpdate();
    }

    void TimeUpdate()
    {
        if (isHour24)
        {
            alarmText[0].text = string.Format("{0:D2} : {1:D2}", alarmData.alarmTime[0], alarmData.alarmTime[1]);
        }
        else
        {
            if (alarmData.isAM) { meridiem = "AM"; }
            else { meridiem = "PM"; }

            alarmText[0].text = string.Format("{0:D2} : {1:D2} <b><size=60>{2}</size></b>",
                alarmData.alarmTime[0], alarmData.alarmTime[1], meridiem);
        }
    }

    void DayUpdate()
    {
        if (!alarmData.isAlarmDay.Contains(false)) { alarmText[1].text = "<color=#429AFF>����</color>"; }

        else if (alarmData.isAlarmDay[0] && alarmData.isAlarmDay[1] && alarmData.isAlarmDay[2]
            && alarmData.isAlarmDay[3] && alarmData.isAlarmDay[4]) { alarmText[1].text = "<color=#429AFF>����</color>"; }

        else if (alarmData.isAlarmDay[5] && alarmData.isAlarmDay[6]) { alarmText[1].text = "<color=#429AFF>�ָ�</color>"; }

        else
        {
            alarmText[1].text = null;

            for (int i = 0; i < alarmData.isAlarmDay.Count; i++)
            {
                if (!alarmData.isAlarmDay[i]) { alarmText[1].text += " " + alarmDay[i]; }
                else { alarmText[1].text += string.Format(" <color=#B4B4B4>{0}</color>", alarmDay[i]); }
            }
        }
    }
}
