using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class BackGroundMover : MonoBehaviour
    {

        //フォーカスの着地点
        private Vector2[] m_movePoints;

        //フォーカス中かどうか
        private bool m_isFocus;

        public void Initialize()
        {
            m_isFocus = false;

            //頭に初期座標を入れたいため、子供の数＋１で領域を確保
            m_movePoints = new Vector2[transform.childCount + 1];

            //配列の頭には0座標を入れる(初期座標的な扱い)
            m_movePoints[0] = Vector2.zero;

            int i = 1;
            //子供になっているポイント達を取得
            foreach(Transform child in transform)
            {
                m_movePoints[i] = -child.GetComponent<RectTransform>().localPosition;
                m_movePoints[i].x += 300;//ポイントが画面右にいい感じに映るように補正
                i++;
            }
            
        }

        /// <summary>
		/// 指定したステージのビジュアルにフォーカス
		/// </summary>
        public void FocusAtPoint(uint arg_pointNumber)
        {
            //すでにフォーカス中なら終了
            if (m_isFocus) return;

            //ステージ番号は1000代の数字で来るので、1000で割ってステージ番号とする
            int point = int.Parse((arg_pointNumber / 1000).ToString());
            
            //すでにフォーカスさせたい位置にいれば速攻で終了
            if (transform.localPosition == (Vector3)m_movePoints[point]) return;

            m_isFocus = true;

            Hashtable hashtable = new Hashtable();
            hashtable.Add("position", (Vector3)m_movePoints[point]);
            hashtable.Add("islocal", true);//ローカル座標に変換
            hashtable.Add("time", 1);//Tweenの時間
            hashtable.Add("oncomplete", "FocusEnd");//Tween終了時に呼ばれる処理
            hashtable.Add("oncompletetarget", gameObject);//↑の処理を持っているオブジェクトを明記

            iTween.MoveTo(gameObject, hashtable);
        }
                
        //フォーカス中かどうかを取得
        public bool IsFocusing()
        {
            return m_isFocus;
        }

        //Tween終了時に呼ばれる処理
        private void FocusEnd()
        {
            m_isFocus = false;
        }
    }

}