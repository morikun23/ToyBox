using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class Jumpramp : MonoBehaviour
    {
        [Range(5,12)]
        public float m_jumpPower;

        Animator m_anim;

        void Start()
        {
            m_anim = GetComponent<Animator>();
        }

        //プレイヤがジャンプ台に乗ったとき
        void OnCollisionEnter2D(Collision2D col)
        {
            GameObject m_playercol;
            m_playercol = col.gameObject;

            ContactPoint2D contact = col.contacts[0];

            //プレイヤが上から台にに乗ったとき大ジャンプ
            if (contact.normal.y <= -1)
            {
                //アニメーションを遷移
                m_anim.SetTrigger("Jump");

                //velocityでジャンプ量を制御
                m_playercol.GetComponent<Rigidbody2D>().velocity = new Vector2(0, m_jumpPower);

            }
        }
    }
}