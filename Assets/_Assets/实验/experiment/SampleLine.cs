using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace n1
{
    public class SampleLine : MonoBehaviour
    {

        public float a;
        [SerializeField] private Transform A;
        [SerializeField] private Transform B;
        [SerializeField] private Transform C;
        [SerializeField] private Transform D;
        [SerializeField] private Transform AB;
        [SerializeField] private Transform BC;
        [SerializeField] private Transform CD;
        [SerializeField] private Transform AC;
        [SerializeField] private Transform BD;
        [SerializeField] private Transform AD;

        private void Update()
        {
            a = (a + Time.deltaTime) % 1;
            AB.position = Vector3.Lerp(A.position, B.position, a);
            BC.position = Vector3.Lerp(B.position, C.position, a);
            CD.position = Vector3.Lerp(C.position, D.position, a);
            AC.position = Vector3.Lerp(AB.position, BC.position, a);
            BD.position = Vector3.Lerp(BC.position, CD.position, a);
            AD.position = Vector3.Lerp(AC.position, BD.position, a);
        }
    }
}

