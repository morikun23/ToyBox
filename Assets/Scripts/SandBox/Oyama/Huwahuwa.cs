using UnityEngine;
using System.Collections;

namespace ToyBox
{

    public class Huwahuwa : MonoBehaviour
    {

        public float num = .2f;
        float startY;

        void Start()
        {
            startY = transform.position.y;
        }

        void Update()
        {
            transform.position = new Vector3(transform.position.x
             , startY + (Mathf.Sin(Time.time * num) / 10), transform.position.z);
            

        }
    }

}
