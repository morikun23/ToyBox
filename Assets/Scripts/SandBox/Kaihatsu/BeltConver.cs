using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class BeltConver : MonoBehaviour
    {
        //オブジェクト検出フラグ
        bool colFlg;

        Rigidbody2D body;

        public float m_MoveSpeed=0.05f;

        void OnCollisionEnter2D(Collision2D col)
        {
            //触れたオブジェクトのRigitbody取得
            body = col.gameObject.GetComponent<Rigidbody2D>();

            //ベルコンに乗った瞬間true
            colFlg = true;
        }

        void OnCollisionExit2D(Collision2D col)
        {
            //離れてfalse
            colFlg = false;
        }

        void UpdateByFrame()
        {
            //ベルコンに乗ってる間は移動
            if (colFlg == true)
            {
                body.transform.position += new Vector3(m_MoveSpeed, 0, 0);
            }
        }

        void Update()
        {
            UpdateByFrame();
        }
    }
}
