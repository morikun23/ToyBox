using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class JumpRamp : MonoBehaviour
    {
        [Range(5,12)]
        public float m_jumpPower;

        private Animator m_anim;

        private GameObject m_colObject;   //衝突したゲームオブジェクト格納用

        void Start()
        {
            m_anim = GetComponent<Animator>();
        }

        //プレイヤがジャンプ台に乗ったとき
        void OnCollisionEnter2D(Collision2D col)
        {

            m_colObject = col.gameObject;

            ContactPoint2D contact = col.contacts[0];

            //プレイヤが上から台に乗ったとき大ジャンプ
            if (contact.normal.y <= -1)
            {
                HighJump();
            }
        }

        void HighJump()
        {
            //アニメーションを遷移
            m_anim.SetTrigger("Jump");

            //velocityでジャンプ量を制御
            m_colObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, m_jumpPower);
        }
    }
}