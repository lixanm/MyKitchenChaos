using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    //识别类型

    [SerializeField]private KitchenObjectSO kitchenObjectSO;

    //确保厨房对象知道自己的位置
    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    //改变父级
    public void SetClearCounter(ClearCounter clearCounter)
    {
        this.clearCounter?.ClearKitchenObject();//清除原来的父级
        
        this.clearCounter = clearCounter;

        if(clearCounter.HasKitchenObject())
        {
            Debug.LogError("柜台上已经有厨房对象了");
        }

        clearCounter.SetKitchenObject(this);//设置新的父级

        transform.parent=clearCounter.GetKitchenObjectFollowTransform();//设置父级位置
        transform.localPosition = Vector3.zero;
    }
    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
