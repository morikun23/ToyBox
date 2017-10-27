using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class Dialog : MonoBehaviour
    {
        Signboard m_signboard;

        GameObject m_signboard_obj;

        float m_openNum;


        void Start()
        {
            //タグ参照(※看板には"Singboard"タグをつける必要有)
            m_signboard_obj = GameObject.FindWithTag("Signboard");

            m_signboard = m_signboard_obj.GetComponent<Signboard>();
            

            m_openNum = 0;
        }

        //ダイアログを一定値まで伸縮
        public void Extend()
        {
            m_openNum += Time.deltaTime / 2;

            if (transform.localScale.x < 0.4f)
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