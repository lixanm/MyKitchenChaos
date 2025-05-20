using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    [SerializeField] private Player player;

    private Vector3 moveDirection;//角色移动方向





    float spinSpeed = 15f;//角色旋转速度
    [SerializeField] private float moveSpeed = 5f;//移动速度

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
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.z += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.z -= 1;
        }

        player.transform.position = player.transform.position + moveDirection.normalized * moveSpeed * Time.deltaTime;

        //玩家运动的旋转,
        //transform.
        //if(moveDirection!=Vector3.zero)
        //{
        //    float spinSpeed = 5f;
        //    //player.transform.right = moveDirection;
        //    // 旋转
        //    player.transform.position = Vector3.Slerp(
        //        player.transform.position,
        //        moveDirection,
        //        spinSpeed * Time.deltaTime
        //    );
        //}


        //todo:读取玩家移动的方向
        player.transform.forward = Vector3.Lerp(player.transform.forward, moveDirection, spinSpeed * Time.deltaTime);

    }
}
