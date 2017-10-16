using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Kaihatsu
{
    public class Signboard_Test : MonoBehaviour
    {

        public GameObject prefab;           //生成するオブジェクト

        GameObject m_DiaPrefab;               //生成したオブジェクト格納

        private Vector3 clickPos;           //クリック位置

        public Vector3 FormPos;             //オブジェクトを生成する場所

        bool m_clickflg = true;               //クリックした際にオブジェクト生成できるか判定

        public bool ClickTrigger = true;    //再び伸縮させる参照渡しフラグ    
        

        void UpdateByFrame()
        {


            //生成したプレハブが消えたら再びフラグをONにする
            if (m_DiaPrefab == null)
            {
                m_clickflg = true;
                ClickTrigger = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                //フラグオフ時(プレハブ生成時)に再びクリックしたら
                //生成したダイアログを収縮させるためのトリガー
                if (m_clickflg == false)
                {
                    ClickTrigger = false;
                }

                //クリック座標を取得
                clickPos = Input.mousePosition;

                //ワールド座標に変換
                clickPos = Camera.main.ScreenToWorldPoint(clickPos);

                //タッチをした位置にオブジェクトがあるかどうかを判定
                RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero);

                //オブジェクトにあたったらヒット
                if (hit)
                {

                    if (m_clickflg == true)
                    {
                        //クリックフラグが真だったらアタッチされたプレハブ生成
                        m_DiaPrefab = Instantiate(prefab, FormPos, Quaternion.identity);

                        //1度プレハブ生成されたらフラグをオフにする
                        m_clickflg = false;

                    }
                }
            }
        }


        // Update is called once per frame
        void Update()
        {
            UpdateByFrame();
        }
    }
}