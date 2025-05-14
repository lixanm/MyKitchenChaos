using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    //切割后的厨房对象类型
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                //让玩家将物体放于柜台上
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                //让柜台上的物体被玩家拾取
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            //切割厨房对象
            //销毁柜台上的物体，再生成一个新的物体
            GetKitchenObject().DestroySelf();

            //生成切好的厨房对象到切割台上
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);

        }
    }



}
