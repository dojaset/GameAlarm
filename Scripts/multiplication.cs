using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UGUI ���ӽ����̽�

public class multiplication : MonoBehaviour
{
    int gugu; // ���� ���� ����
    int dan; // ������ ����
    int gugudan; // ���� ���ؼ� ���� ����

    public Text guguText; // ���� ���� �ؽ�Ʈ
    public Text danText; //������ ���� ���� �ؽ�Ʈ
    public InputField inputField; // ��ǲ�ʵ�
    // Start is called before the first frame update
    void Start()
    {
        gugu= Random.Range(2, 10); // 2���� 9������ ���� ��
        dan = Random.Range(1, 10); // 1���� 9������ ���� ��
        gugudan = gugu * dan;

        guguText.text = gugu.ToString(); // int�� string���� �ٲ� ����
        danText.text = dan.ToString(); 

        Debug.Log(gugu + "��" + dan + "�� ����" + gugudan +"�Դϴ�");    
    }

    public void GuguDan()
    {
        if(inputField.text == gugudan.ToString()) // ��ǲ�ʵ� �ؽ�Ʈ�� �����̶�� ������ ��� ����
        {
            gugu = Random.Range(2, 10); // 2���� 9������ ���� ��
            dan = Random.Range(1, 10); // 1���� 9������ ���� ��
            gugudan = gugu * dan;

            guguText.text = gugu.ToString(); // int�� string���� �ٲ� ����
            danText.text = dan.ToString();

            Debug.Log(gugu + "��" + dan + "�� ����" + gugudan + "�Դϴ�");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
