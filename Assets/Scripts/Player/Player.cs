using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs>  OnSelectedCounterChanged;
    //�¼�����
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;


    private bool isWalking;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Player instance is not null");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter!=null)
        {
            selectedCounter.Interact();//���ý�������
        }

    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        //���»�ȡ����ֵ���������
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();//��ȡ����ķ�������
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if(moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;//��������뷽����������Ľ�������
        }

        float interactDistance = 2f;//��������

        if (Physics.Raycast(
            transform.position,//λ��
            lastInteractDir, //����
            out RaycastHit raycastHit,//���߼�⵽������
            interactDistance, 
            countersLayerMask)
        )
        {
            //��⵽����
            if(raycastHit.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter))
            {
                //��ClearCounter�ű�
                //clearCounter.Interact();//���ý�������
                if(clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
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
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast
            (
                transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius,
                moveDirX,
                moveDistance
            );
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast
                (
                    transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius,
                    moveDirZ,
                    moveDistance
                );
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    moveDir = Vector3.zero;
                }
            }
        }

        if (canMove)
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
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationalSpeed);//�������ǰ����������Ϊ�ƶ�����

    }
    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        //ѡ�����ı�ʱ�������¼��ʹ�����Ϣ
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
