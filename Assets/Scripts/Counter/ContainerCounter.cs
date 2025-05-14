using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家与之互动，生成食材到玩家手中
public class ContainerCounter : BaseCounter
{

    //创建一个事件，触发时可以播放动画
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
            return;
        }

        //生成厨房对象到玩家手中
        KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);


        OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);
    }
}
