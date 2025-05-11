using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField]private KitchenObjectSO kitchenObjectSO;
    [SerializeField]private Transform counterToPoint;

    public void Interact()
    {
        //Debug.Log("½»»¥");
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterToPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;

        Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
    }
}
