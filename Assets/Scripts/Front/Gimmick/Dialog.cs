using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class Dialog : MonoBehaviour
    {
        Signboard m_signboard;

        float m_openNum;


        public void Init(Signboard arg_sign)
        {
            m_signboard = arg_sign;
            m_openNum = 0;
            
        }

        //ダイアログを一定値まで伸縮
        public void Extend()
        {
            m_openNum += Time.deltaTime / 2;

            if (transform.localScale.x < 0.3f)
            {
                transform.localScale += new Vector3(m_openNum, 0, 0);
            }
            else
            {
                m_openNum = 0;
                transform.localScale = new Vector3(0.4f, 0.4f, 1);
            }
        }

        //ダイアログを一定値まで収縮して削除
        public void Shrink()
        {
            m_openNum += Time.deltaTime * 2;

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
            if(m_signboard.m_ExtendFlg==false)
            Shrink();
        }
        void Update()
        {
            UpdateByFrame();
        }
    }
}