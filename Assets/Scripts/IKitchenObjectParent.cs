using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    //台面点位置
    public Transform GetKitchenObjectFollowTransform();

    //设置获取厨房对象
    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    //清除柜台上的厨房对象
    public void ClearKitchenObject();

    //柜台上是否有厨房对象
    public bool HasKitchenObject();

}
