using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs>  OnSelectedCounterChanged;
    //事件参数
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
            selectedCounter.Interact();//调用交互方法
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
        //重新获取方向值，避免干扰
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();//获取输入的方向向量
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if(moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;//如果有输入方向，则更新最后的交互方向
        }

        float interactDistance = 2f;//交互距离

        if (Physics.Raycast(
            transform.position,//位置
            lastInteractDir, //方向
            out RaycastHit raycastHit,//射线检测到的物体
            interactDistance, 
            countersLayerMask)
        )
        {
            //检测到物体
            if(raycastHit.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter))
            {
                //有ClearCounter脚本
                //clearCounter.Interact();//调用交互方法
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
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();//获取输入的方向向量
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;//移动距离
        float playerRadius = .7f;//玩家的半径
        float playerHeight = 2f;//玩家高度

        //bool canMove = !Physics.Raycast(transform.position, moveDir, playerSize);//发射射线检测是否有物体挡在路上

        bool canMove = !Physics.CapsuleCast
        (
            transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius,
            moveDir,
            moveDistance
        );//胶囊投射//斜向碰撞无法移动

        //解决“斜向碰撞无法移动”的问题
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
            transform.position += moveDir * moveDistance;//乘以一帧的秒数
        }

        isWalking = moveDir != Vector3.zero;//是否在运动，这个变量将影响到动画

        //transform.rotation 修改四元数来旋转
        //修改变换的欧拉角
        //transform.eulerAngles += new Vector3(0, inputVector.x * 5, 0);
        //transform.forward//表示外部轴的归一化向量，读取可获取到当前物体的前方方向，写入可设置物体的前方方向
        //transform.up;
        //transform.right;//对2D游戏有用，和transform.forward和transform.up相似

        //transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y+ 90, 0);
        //transform.eulerAngles += new Vector3(0, inputVector.x * 5, 0);//绕y轴旋转
        //transform.LookAt(transform.position);//朝向一个点

        float rotationalSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationalSpeed);//将物体的前方方向设置为移动方向

    }
    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        //选择对象改变时，触发事件和传递信息
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
