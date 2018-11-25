using JetBrains.Annotations;
using UnityEngine;

namespace DefaultNamespace
{
    public class Rotate : MonoBehaviour
    {
        public Vector3 axis;
        public float amount;

        public void Update()
        {
            this.transform.Rotate(axis, amount * Time.deltaTime);
        }
    }
}