using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class TutorialDialog : MonoBehaviour
    {
        private Signboard m_signboard;

        private float m_openNum;

        private Vector3 m_endSize;

        private float m_worldScreenHeight;
        private float m_worldScreenWidth;
        
        private float m_width;
        private float m_height;

        public void Init(Signboard arg_sign)
        {
            m_signboard = arg_sign;
            m_openNum = 0;

            //画面の比率に合わせてスプライトの大きさを変える　ネットからパクリスペクト

            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            // カメラの外枠のスケールをワールド座標系で取得
            m_worldScreenHeight = Camera.main.orthographicSize * 2f;
            m_worldScreenWidth = m_worldScreenHeight / Screen.height * Screen.width;

            // スプライトのスケールもワールド座標系で取得
            m_width = sr.sprite.bounds.size.x;
            m_height = sr.sprite.bounds.size.y;

            //  両者の比率を出してスプライトのローカル座標系に反映
            transform.localScale = new Vector3(m_worldScreenWidth / m_width, m_worldScreenHeight / m_height);
            m_endSize = transform.localScale - new Vector3(transform.localScale.x / 5, transform.localScale.y / 5, 0);

            transform.localScale = new Vector3(0, m_endSize.y, 1);
        }

        //ダイアログを一定値まで伸縮
        public void Extend()
        {
            m_openNum += m_endSize.x / 100;

            if (transform.localScale.x < m_endSize.x - (m_endSize.x / 10))
            {
                transform.localScale += new Vector3(m_openNum, 0, 0);
            }
            else
            {
                m_openNum = 0;
                transform.localScale = new Vector3(m_endSize.x, m_endSize.y - (m_endSize.y / 20), 1);
            }
        }

        //ダイアログを一定値まで収縮して削除
        public void Shrink()
        {
            m_openNum += m_endSize.x / 100;

            transform.localScale += new Vector3(-m_openNum, 0, 0);

            if (transform.localScale.x < 0)
            {
                Destroy(gameObject);
            }
        }
        void UpdateByFrame()
        {
            //アームが触れてるときダイアログ伸縮
            if (m_signboard.m_ExtendFlg == true)
            Extend();

            //アームが離れたときダイアログ収縮
            if(m_signboard.m_ExtendFlg== false)
            Shrink();
        }
        void Update()
        {
            UpdateByFrame();
        }
    }
}