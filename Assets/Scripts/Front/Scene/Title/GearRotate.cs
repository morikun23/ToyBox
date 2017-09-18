using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Title
{
    public class GearRotate : MonoBehaviour
    {

        float m_addRotate;

        //初期化関数
        public void Init(float arg_addRotate)
        {
            m_addRotate = arg_addRotate;
        }

        // Update is called once per frame
        public void OnUpdate()
        {
            transform.Rotate(0, 0, m_addRotate);
        }
    }
}