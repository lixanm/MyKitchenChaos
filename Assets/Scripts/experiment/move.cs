using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

namespace n1
{
    public class move : MonoBehaviour
    {
        

        [SerializeField] private float speed;

        private void Update()
        {
            Vector2 position = Vector2.zero;

            if(Input.GetKey(KeyCode.W))
            {
                position.x = position.x + 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                position.x = position.x - 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                position.y = position.y - 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                position.y = position.y + 1;
            }
            position = position.normalized;
            Vector3 middle = new Vector3(position.y, 0, position.x);
            transform.position += middle * Time.deltaTime * speed;
        }

    }
}

