using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class AlarmSetting : MonoBehaviour
{
    AlarmData alarmData; //알람 데이터 파일

    [Header("상단바 버튼")]
    public Button[] topBarButtons; //0 = 취소, 1 = 삭제, 2 = 확인

    [Space]
    [Header("알람 시간 설정")]
    public TextMeshProUGUI[] alarmTimeText; //0 = AM/PM, 1 = 시간, 2 = 분
    public TextMeshProUGUI remainTimeText;  //다음 알람까지 남은 시간 텍스트
    DateTime alarmDateTime; //화면상의 DateTime. 실제 데이터와는 상관 X
    TimeSpan remainTime;    //위 변수와 현재 시간의 차이을 계산하기 위한 변수

    [Space]
    [Header("반복 설정")]
    public Toggle[] repeatToggles; //요일 토글
    public Button calanderButton;  //달력 버튼

    [Space]
    [Header("소리 및 진동 설정")]
    public Toggle[] soundToggles; //0 = 소리, 1 = 진동
    public TextMeshProUGUI soundNameText; //알람음 이름

    [Space]
    [Header("알람 간격 / 미루기")]
    public TextMeshProUGUI intervalText; //간격 빈도 표시 텍스트
    public TextMeshProUGUI delayText;    //미루기 빈도 표시 텍스트
    int intervalMin;  int intervalNum;   //간격 분, 횟수
    int delayMin;     int delayNum;      //미루기 분, 횟수

    [Space]
    [Header("그 외")]
    public Scrollbar scrollbar;          //화면 전체 스크롤바
    public TextMeshProUGUI labelText;    //라벨 텍스트
    public TextMeshProUGUI gameNameText; //게임 종류 텍스트
    public Toggle holidayToggle;         //휴가 모드 토글

    void Awake()
    {
        if (GameManager.Ins.alarmData) { alarmData = GameManager.Ins.alarmData; }
        else { alarmData = ScriptableObject.CreateInstance<AlarmData>(); }
        //알람 수정: 매니저로부터 데이터 파일을 건네받음
        //신규 생성: 임시 데이터 파일 생성
    }

    void Start() 
    {
        scrollbar.value = 1.0f; //스크롤바 값 초기화

        if (!GameManager.Ins.is24Hour) { alarmTimeText[0].gameObject.SetActive(false); }
        else { alarmTimeText[0].text = alarmData.meridiem; }
        //AM, PM 표시 (24시간제 설정 시 오브젝트 미사용)

        alarmTimeText[1].text = alarmData.alarmHour.ToString();
        alarmTimeText[2].text = alarmData.alarmMinute.ToString();
        //알람 시간, 분 표시 -> 지금 InputField문제로 잘 안됨

        for (int i = 0; i < repeatToggles.Length; i++) { repeatToggles[i].isOn = alarmData.repeatDay[i]; }
        //요일별 반복 상태 표시

        if (alarmData.alarmLabel != null)
        {
            labelText.text = alarmData.alarmLabel;
            labelText.color = new(0, 0, 0, 255);
            //라벨 텍스트 표시 및 컬러 변경(블랙)
        }
        else
        {
            labelText.text = "알람 화면에 표시";
            labelText.color = new Color(0, 0, 0, 150);
            //라벨 설명 표시 및 컬러 변경(그레이)
        }

        if (alarmData.audioClip) //알람음 파일 有
        {
            soundNameText.text = alarmData.audioClip.name;
            soundToggles[0].isOn = alarmData.soundMode;
            //알람음 이름 텍스트, 활성 상태 표시
        }
        else
        {
            soundNameText.text = "무음";
            soundToggles[0].isOn = false;
            //미선택시 무음 처리 및 토글 off
        }

        soundToggles[1].isOn = alarmData.vibrationMode; //진동 활성 상태 표시

        gameNameText.text = alarmData.gameType; //게임 이름 텍스트 표시

        intervalMin = alarmData.intervalMin;
        intervalNum = alarmData.intervalNum;
        //반복 설정값 복사

        delayMin = alarmData.delayMin;
        delayNum = alarmData.delayNum;
        //미루기 설정값 복사

        if (intervalMin == 0 || intervalNum == 0) { intervalText.text = "사용하지 않음"; }
        else { intervalText.text = string.Format("{0}분 간격으로 {1}번", intervalMin, intervalNum); }
        //반복 설정값 텍스트 표시

        if (delayMin == 0 || delayNum == 0) { delayText.text = "사용하지 않음"; }
        else { delayText.text = string.Format("{0}분 간격으로 {1}번", delayMin, delayNum); }
        //미루기 설정값 텍스트 표시

        holidayToggle.isOn = alarmData.holidayMode;
        //휴가모드 활성 상태 표시
    }

    void Update()
    {
        GameManager.Ins.ShowRemainTime(alarmDateTime, remainTimeText);
    }

    public void SaveAlarmSetting() //변경 내용을 AlarmData에 반영
    {
        //alarmData.alarmHour = int.Parse(alarmTimeText[1].text);
        //alarmData.alarmMinute = int.Parse(alarmTimeText[2].text);
        //알람 시간 설정값 반영 -> InputField 문제로 안됨
        
        alarmData.alarmDateTime = new DateTime(2023, 4, 5, alarmData.alarmHour, alarmData.alarmMinute, 0);
        //DateTime 임시 지정

        for (int i = 0; i < repeatToggles.Length; i++)
        {
            alarmData.repeatDay[i] = repeatToggles[i].isOn;
            //요일별 반복 설정값 반영
        }

        alarmData.alarmLabel = labelText.text;
        //라벨 반영

        //alarmData.audioClip
        alarmData.soundMode = soundToggles[0].isOn;
        alarmData.vibrationMode = soundToggles[1].isOn;
        //소리 및 진동 설정값 반영

        alarmData.gameType = gameNameText.text;
        //게임 종류 반영

        alarmData.intervalMin = intervalMin;
        alarmData.intervalNum = intervalNum;
        //간격 설정값 반영

        alarmData.delayMin = delayMin;
        alarmData.delayNum = delayNum;
        //미루기 설정값 반영

        alarmData.holidayMode = holidayToggle.isOn;
        //휴가모드 설정값 반영

        alarmData.updateDate = DateTime.Now.ToString("yy-MM-dd HH mm ss");
        //마지막 변경일자 반영

        if (alarmData.createDate == null) { CreateAlarmDataFile(); }
    }

    void CreateAlarmDataFile()
    {
        alarmData.createDate = DateTime.Now.ToString("yy-MM-dd HH mm ss");
        //생성일자 변경

        string filename = "Assets/UserDatas/" + alarmData.createDate + ".asset";
        //파일 경로, 이름 양식 지정

        AssetDatabase.CreateAsset(alarmData, filename);
        AssetDatabase.SaveAssets();
        //파일 생성, 지정 양식에 따라 저장

        GameManager.Ins.alarmData = alarmData;
        //매니저 오브젝트에 데이터 파일 전달

        GameManager.Ins.isNewAlarm = true;
        //새 프리팹 생성이 필요한 상태로 전환
    }
}