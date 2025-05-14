using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//柜台父类，使角色与柜台的逻辑更易处理
public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    //[SerializeField] private KitchenObjectSO kitchenObjectSO;//基础柜台不需要接触对象，所以注释此行
    [SerializeField] private Transform counterToPoint;

    //柜台要清楚是否有厨房对象放在上面
    private KitchenObject kitchenObject;


    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact()");
    }

    public virtual void InteractAlternate(Player player)
    {
        Debug.LogError("BaseCounter.InteractAlternate()");
    }

    //台面点位置
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterToPoint;
    }

    //设置获取厨房对象
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    //清除柜台上的厨房对象
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    //柜台上是否有厨房对象
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
