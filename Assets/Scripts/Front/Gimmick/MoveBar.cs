using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;      //!< デプロイ時にEditorスクリプトが入るとエラーになるので UNITY_EDITOR で括ってね！
#endif

namespace ToyBox
{

    public class MoveBar : MonoBehaviour
    {

        //端
        Vector2 m_StartPoint, m_EndPoint;

        //動くバー
        GameObject m_Bar;

        //最大速度
        [SerializeField]
        [Range(0.05f,0.1f)]
        float m_moveMaxNum;

        //実際の移動値
        float m_moveNumReal;

        Vector2 nowPos, targetPos;

        [SerializeField]
        bool m_actionFlag = true;

        private Rigidbody2D m_rigidbodyOfBar;

        //現在位置からターゲットまでの割合
        float m_currentPosInverse;

        //現在位置からターゲットまでの距離
        float m_currentDistance;

        //移動値を加算させる回数
        int m_count = 0;

        // Use this for initialization
        void Start()
        {
            nowPos = targetPos = Vector2.zero;

            //各種オブジェの参照
            m_StartPoint = transform.Find("SP_MoveArea/StartPoint").transform.position;
            m_EndPoint = transform.Find("SP_MoveArea/EndPoint").transform.position;

            m_Bar = transform.Find("Bar").gameObject;
            m_Bar.transform.position = m_StartPoint;

            targetPos = m_EndPoint;

            m_rigidbodyOfBar = m_Bar.GetComponent<Rigidbody2D>();

            m_moveNumReal = 0;
            //ターゲットまでの距離を算出
            m_currentDistance = Vector2.Distance(m_Bar.transform.position, targetPos);

            //AudioSource source = AppManager.Instance.m_audioManager.CreateBgm ("SE_BeltConveyor_move");
            //source.Play ();
            //source.loop = true;

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!m_actionFlag) return;

            //ターゲットまでの距離と、現在位置の割合を算出
            m_currentPosInverse = Mathf.InverseLerp(0, m_currentDistance, Vector2.Distance(m_Bar.transform.position, targetPos));

            //近いようであれば移動値を加算
            if (m_currentPosInverse > 0.7f && m_moveNumReal != m_moveMaxNum)
            {
                m_moveNumReal += m_moveMaxNum / 100;
            }

            //遠いようであれば移動値を減算
            if (m_currentPosInverse < 0.3f && m_moveNumReal > m_moveMaxNum / 100)
            {
                m_moveNumReal -= m_moveMaxNum / 100;
            }

            
            //リジッドボディに加える力の量を決定
            nowPos = Vector2.MoveTowards(m_Bar.transform.position, targetPos, m_moveNumReal);

            //ターゲットまでの距離が縮まったらターゲットを入れ替える
            if (Vector2.Distance( nowPos,targetPos) < 0.01f)
            {
                if (targetPos == m_StartPoint)
                {
                    targetPos = m_EndPoint;
                }
                else
                {
                    targetPos = m_StartPoint;
                }
                
                //移動値のリセットと新しいターゲットまでの距離を更新
                m_moveNumReal = 0;
                m_currentDistance = Vector2.Distance(m_Bar.transform.position, targetPos);
            }
            else
            {
                //バーの移動
                m_rigidbodyOfBar.MovePosition(nowPos);
            }
        }

        //外部(スイッチとか？)から作動を開始させたい場合に呼ぶやつ
        public void Action()
        {
            m_actionFlag = true;
        }

#if UNITY_EDITOR
        /**
         * Inspector拡張クラス
         */
        [CustomEditor(typeof(MoveBar))]
        public class MoveBarEditor : Editor           //!< Editorを継承するよ！
        {
            MoveBar moveBar;
            GameObject moveArea;

            GameObject m_StartPointObj;
            GameObject m_EndPointObj;

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                //注意書き
                EditorGUILayout.LabelField("※横向きで置きたい場合");
                EditorGUILayout.LabelField("GM_MoveBarのRotationを０にした状態で押して回転してね");
                //ボタンを追加
                if (GUILayout.Button("Set->StartPos&EndPos"))
                {

                    this.moveBar = target as MoveBar;
                    moveArea = moveBar.transform.Find("SP_MoveArea").gameObject;

                    m_StartPointObj = moveArea.transform.Find("StartPoint").gameObject;
                    m_EndPointObj = moveArea.transform.Find("EndPoint").gameObject;

                    m_StartPointObj.transform.position = moveArea.transform.position + new Vector3(0, (moveArea.GetComponent<SpriteRenderer>().bounds.size.y / 2) - 0.3f, 0);
                    m_EndPointObj.transform.position = moveArea.transform.position - new Vector3(0, (moveArea.GetComponent<SpriteRenderer>().bounds.size.y / 2) - 0.3f, 0);
                }

            }
        }
#endif

    }
}