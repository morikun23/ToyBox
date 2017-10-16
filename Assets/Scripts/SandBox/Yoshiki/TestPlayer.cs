using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Yoshiki
{

    public class TestPlayer : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0f, 0.1f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-0.05f, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(0.05f, 0);
            }
        }
    }
}