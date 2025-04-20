using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private bool isWalking;

    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }
        inputVector=inputVector.normalized;//��һ��
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        isWalking = moveDir != Vector3.zero;//�Ƿ����˶������������Ӱ�쵽����

        transform.position += moveDir * Time.deltaTime * moveSpeed;//����һ֡������

        //transform.rotation �޸���Ԫ������ת
        //�޸ı任��ŷ����
        //transform.eulerAngles += new Vector3(0, inputVector.x * 5, 0);
        //transform.forward//��ʾ�ⲿ��Ĺ�һ����������ȡ�ɻ�ȡ����ǰ�����ǰ������д������������ǰ������
        //transform.up;
        //transform.right;//��2D��Ϸ���ã���transform.forward��transform.up����

        //transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y+ 90, 0);
        //transform.eulerAngles += new Vector3(0, inputVector.x * 5, 0);//��y����ת
        //transform.LookAt(transform.position);//����һ����

        float rotationalSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,moveDir,Time.deltaTime* rotationalSpeed);//�������ǰ����������Ϊ�ƶ�����

    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
