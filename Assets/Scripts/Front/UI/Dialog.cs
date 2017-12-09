using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class Dialog : MonoBehaviour
    {
        Signboard m_signboard;

        float m_openNum;

        Vector3 endSize;

        float worldScreenHeight;
        float worldScreenWidth;
        
        float width;
        float height;

        public void Init(Signboard arg_sign)
        {
            m_signboard = arg_sign;
            m_openNum = 0;

            //画面の比率に合わせてスプライトの大きさを変える　ネットからパクリスペクト

            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            // カメラの外枠のスケールをワールド座標系で取得
            worldScreenHeight = Camera.main.orthographicSize * 2f;
            worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            // スプライトのスケールもワールド座標系で取得
            width = sr.sprite.bounds.size.x;
            height = sr.sprite.bounds.size.y;

            //  両者の比率を出してスプライトのローカル座標系に反映
            transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height);
            endSize = transform.localScale;

            transform.localScale = new Vector3(0, endSize.y - (endSize.y / 20), 1);
        }

        //ダイアログを一定値まで伸縮
        public void Extend()
        {
            m_openNum += endSize.x / 100;

            if (transform.localScale.x < endSize.x - (endSize.x / 10))
            {
                transform.localScale += new Vector3(m_openNum, 0, 0);
            }
            else
            {
                m_openNum = 0;
                transform.localScale = new Vector3(endSize.x, endSize.y - (endSize.y / 20), 1);
            }
        }

        //ダイアログを一定値まで収縮して削除
        public void Shrink()
        {
            m_openNum += endSize.x / 100;

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