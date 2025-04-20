using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

namespace n1
{
    public class move : MonoBehaviour
    {


        [SerializeField] private float moveSpeed = 5f;

        private void Update()
        {
            Vector2 position = new Vector2(0,0);

            if(Input.GetKey(KeyCode.W))
            {
                position.y = position.y + 1;
            }
            if (Input.GetKey(KeyCode.S))
            { 
                position.y = position.y - 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                position.x = position.x - 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                position.x = position.x + 1;
            }
            position = position.normalized;
            Vector3 middle = new Vector3(position.x, 0, position.y);
            transform.position += middle * Time.deltaTime * moveSpeed;

            

            float rotationalSpeed = 5f;
            transform.forward = Vector3.Slerp(transform.forward, middle, Time.deltaTime * rotationalSpeed);



        }

    }
}

