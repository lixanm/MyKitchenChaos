using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";//动画参数名称

    [SerializeField] private GameInput gameInput;
    private Animator animator;

    private void Awake()
    {
        //获取Animator组件
        animator = GetComponent<Animator>();
    }

    //如果角色在运动，触发动画
    private void Update()
    {
        if (gameInput.IsWalking())
        {
            //触发动画
            animator.SetBool(IS_WALKING, true);
        }
        else
        {
            animator.SetBool(IS_WALKING, false);
        }

    }


}
