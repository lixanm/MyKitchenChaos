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
        inputVector=inputVector.normalized;//归一化
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        isWalking = moveDir != Vector3.zero;//是否在运动，这个变量将影响到动画

        transform.position += moveDir * Time.deltaTime * moveSpeed;//乘以一帧的秒数

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
        transform.forward = Vector3.Slerp(transform.forward,moveDir,Time.deltaTime* rotationalSpeed);//将物体的前方方向设置为移动方向

    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
