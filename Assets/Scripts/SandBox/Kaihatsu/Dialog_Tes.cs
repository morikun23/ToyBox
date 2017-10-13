using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Kaihatsu
{
    public class Dialog_Tes : MonoBehaviour
    {

        public bool DialogExtend = true;    //ダイアログの伸縮状態(生成時はTrue固定)

        GameObject m_boad;                    //ゲームオブジェクト参照用
        Signboard_Test m_signboard;                //看板オブジェクトの参照


        void Start()
        {
            //タグでゲームオブジェクト取得
            m_boad = GameObject.FindWithTag("Signboard");
            m_signboard = m_boad.GetComponent<Signboard_Test>();
        }

        public void UpdateByFrame()
        {
            //看板側の収縮フラグ参照
            bool SignFlg = m_signboard.ClickTrigger;

            //ダイヤログ伸縮状態がtrueなら一定値までダイヤログ伸縮
            if (DialogExtend == true)
                if (transform.localScale.x < 2.5f)
                {
                    transform.localScale += new Vector3(0.05f, 0, 0);
                }

            //ダイアログが一定の大きさになったら停止
            if (transform.localScale.x > 2.5f)
            {
                DialogExtend = false;
            }

            //ダイヤログ伸縮状態false時
            if (DialogExtend == false)
            {
                //クリック時ダイアログが伸びきっていたら収縮
                if (SignFlg == false)
                {
                    transform.localScale += new Vector3(-0.05f, 0, 0);
                }

                //収縮しきったら削除
                if (transform.localScale.x <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        void Update()
        {
            UpdateByFrame();
        }
    }
}