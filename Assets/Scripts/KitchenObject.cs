using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    //识别类型

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    //确保厨房对象知道自己的位置
    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    //改变父级
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        this.kitchenObjectParent?.ClearKitchenObject();//清除原来的父级

        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("厨房对象的父级已经有厨房对象了");
        }

        kitchenObjectParent.SetKitchenObject(this);//设置新的父级

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();//设置父级位置
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    //可以在切割台上销毁自己，方便生成新的厨房对象
    public void DestroySelf()
    {
        //在销毁自己之前，进入父对象，让父对象也消除自己
        kitchenObjectParent.ClearKitchenObject();

        Destroy(gameObject);
    }


    //生成厨房对象
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObjectTransform.GetComponent<KitchenObject>();
    }



}
