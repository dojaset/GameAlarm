using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UGUI 네임스페이스

public class multiplication : MonoBehaviour
{
    int gugu; // 단이 나올 숫자
    int dan; // 곱해질 숫자
    int gugudan; // 둘이 곱해서 나올 숫자

    public Text guguText; // 단이 나올 텍스트
    public Text danText; //곱해질 수가 나올 텍스트
    public InputField inputField; // 인풋필드
    // Start is called before the first frame update
    void Start()
    {
        gugu= Random.Range(2, 10); // 2부터 9까지의 랜덤 수
        dan = Random.Range(1, 10); // 1부터 9까지의 랜덤 수
        gugudan = gugu * dan;

        guguText.text = gugu.ToString(); // int를 string으로 바꿀 변수
        danText.text = dan.ToString(); 

        Debug.Log(gugu + "와" + dan + "의 곱은" + gugudan +"입니다");    
    }

    public void GuguDan()
    {
        if(inputField.text == gugudan.ToString()) // 인풋필드 텍스트가 정답이라면 문제를 계속 낸다
        {
            gugu = Random.Range(2, 10); // 2부터 9까지의 랜덤 수
            dan = Random.Range(1, 10); // 1부터 9까지의 랜덤 수
            gugudan = gugu * dan;

            guguText.text = gugu.ToString(); // int를 string으로 바꿀 변수
            danText.text = dan.ToString();

            Debug.Log(gugu + "와" + dan + "의 곱은" + gugudan + "입니다");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
