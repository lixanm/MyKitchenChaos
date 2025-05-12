using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterToPoint;


    //将物体转移到第二个柜台
    [SerializeField] private ClearCounter sceondClearCounter;
    [SerializeField] private bool testing;


    //柜台要清楚是否有厨房对象放在上面
    private KitchenObject kitchenObject;

    private void Update()
    {
        //将物体转移到第二个柜台
        if (testing &&Input.GetKeyUp(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(sceondClearCounter);
            }
        }

    }

    //互动
    public void Interact()
    {
        if(kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterToPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObject.GetClearCounter());
        }

        

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
