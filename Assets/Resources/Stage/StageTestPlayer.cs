using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Oyama
{

    public class StageTestPlayer : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * 0.1f, Input.GetAxisRaw("Vertical") * 0.1f, 0));

        }
    }


}