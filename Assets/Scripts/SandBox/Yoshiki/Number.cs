using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class Number : MonoBehaviour
    {
        public bool hitNumber = false;

        void OnTriggerEnter2D(Collider2D other)
        {
            hitNumber = true;
        }
    }
}