using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();//��ȡ����ķ�������

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;//�ƶ�����
        float playerRadius = .7f;//��ҵİ뾶
        float playerHeight = 2f;//��Ҹ߶�

        //bool canMove = !Physics.Raycast(transform.position, moveDir, playerSize);//�������߼���Ƿ������嵲��·��

        bool canMove = !Physics.CapsuleCast
        (
            transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius,
            moveDir, 
            moveDistance
        );//����Ͷ��//б����ײ�޷��ƶ�

        //�����б����ײ�޷��ƶ���������
        if ( !canMove )
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast
            (
                transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius,
                moveDirX,
                moveDistance
            );
            if( canMove )
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast
                (
                    transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius,
                    moveDirZ,
                    moveDistance
                );
                if( canMove )
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    moveDir = Vector3.zero;
                }
            }
        }

        if ( canMove ) 
        { 
            transform.position += moveDir * moveDistance;//����һ֡������
        }

        isWalking = moveDir != Vector3.zero;//�Ƿ����˶������������Ӱ�쵽����

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
