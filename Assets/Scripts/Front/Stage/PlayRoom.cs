using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class PlayRoom : MonoBehaviour
    {

        //こいつの部屋番号 インスペクターで設定
        [SerializeField]
        int m_CurrentRoom;

        //子オブジェクトのギミック達　初期化時に入れ子？
        List<Transform> m_gimmicks = new List<Transform>();
        //List<MonoBehaviour> m_gimmicks = new List<MonoBehaviour>();

        //チェックポイント　ルーム内にあればインスペクターで設定
        [SerializeField]
        CheckPoint[] m_checkPoints;

        // Use this for initialization
        void OnEnable()
        {

            int i = 0;
            //ギミック(子オブジェの参照、作動)
            foreach (Transform gimmick in transform)
            {
                m_gimmicks.Add(gimmick);
                m_gimmicks[i].gameObject.SetActive(true);
                i++;
            }

            Debug.Log(m_gimmicks.Count);
            //ToDo:チェックポイントがあるかどうかのチェックとTrueならStageスクリプトのSet関数を呼ぶ

        }

        // Update is called once per frame
        //void Update()
        //{

        //}


        //コライダーから呼ばれる、現在どのルームに触れているかの関数
        public void SetCurrentRoom()
        {
            //スムーズなカメラ遷移
            CameraPosController.Instance.SetTargetAndStart(m_CurrentRoom);
        }

        //コライダーから呼ばれる、次の部屋へ遷移時にギミックと自分のアクティブを消す
        public void SleepRoomGimmick()
        {
            if (m_gimmicks.Count != 0){
                for (int i = 0; i < m_gimmicks.Count; i++)
                {
                    m_gimmicks[i].gameObject.SetActive(false);
                }
            }
            this.gameObject.SetActive(false);
        }

    }

}