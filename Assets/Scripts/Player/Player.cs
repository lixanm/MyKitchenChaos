using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 moveDirection;//移动方向

    [SerializeField]private float moveSpeed = 5f;//移动速度

    private void Start()
    {

    }

    private void Update()
    {
        moveDirection = new Vector3(0, 0, 0);//移动方向


        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.x -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection.x += 1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            moveDirection.z += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.z -= 1;
        }
        moveDirection = moveDirection.normalized;

        transform.position =transform.position + moveDirection * moveSpeed * Time.deltaTime;

        //todo:玩家运动的旋转
        //transform.


    }
}
