using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class StageSelectInfo : MonoBehaviour
    {

        //何ステージまで解放しているか
        [SerializeField]
        private int m_openStageCount;

        //ステージごと総小部屋数
        [SerializeField]
        private Dictionary<string, int> m_totalRoomCount = new Dictionary<string, int>();

        //ステージごとにどの部屋まで到達しているか
        [SerializeField]
        private Dictionary<string, int> m_arrivalRoomCount = new Dictionary<string, int>();

        const string STAGE = "STAGE";

        //初期化
        public void Initialize()
        {

            //ToDo:ユーザーがどのステージまで解放しているか、どのステージのどこまで到達しているかをNiftyから取得

            //Niftyから、ステージの到達率を取得

            //ステージ１は最初から解放
            m_openStageCount = 1;

            //ステージ１がクリア済みなら２も解放
            if (AppManager.Instance.user.m_temp.m_dic_[0].ContainsKey("Clear"))
                m_openStageCount++;


                //きめうち
                m_totalRoomCount[STAGE + 1] = 9;
            m_totalRoomCount[STAGE + 2] = 13;

            //とりあえず最初の小部屋だけ解放
            for (int i = 1; i < m_openStageCount + 1; i++)
            {
                m_arrivalRoomCount[STAGE + i] = 1;
            }


        }

        /// <summary>
        /// そのユーザーがどのステージまで開放しているかを取得
        /// 返り値はintのステージ数
        /// </summary>
        public int GetOpenStageCount()
        {
            return m_openStageCount;
        }

        /// <summary>
        /// 引数の番号のステージの小部屋の総数を取得
        /// </summary>
        /// <param name="arg_stageNumber">どのステージの</param>
        public int GetTotalRoomCount(int arg_stageNumber)
        {
            return m_totalRoomCount[STAGE + arg_stageNumber];
        }

        /// <summary>
        /// 引数の番号のステージの小部屋の到達率を取得
        /// </summary>
        /// <param name="arg_stageNumber">どのステージの</param>
        public int GetArrivalRoomCount(int arg_stageNumber)
        {
            return m_arrivalRoomCount[STAGE + arg_stageNumber];
        }
    }

}