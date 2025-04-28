using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
}
