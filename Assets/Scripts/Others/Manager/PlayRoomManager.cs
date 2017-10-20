using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class PlayRoomManager : MonoBehaviour
    {

        //ギミックのリスト予定
        //List<Gimmick> 的な

        /////部屋ごとのカメラの座標、Y座標、カメラのSize(どれだけズームするか)を集めた配列//////

        //ステージ１(チュートリアル)
        float[,] Stage1 = { { -51, -6, 4 },
                            { -35, -6, 4 },
                            { -20, -5.5f, 3 },
                            { -11, -3.5f, 4 },
                            { -3, -2.5f, 3.2f },
                            { 10.5f, -1.5f, 4.2f },
                            { 21.5f, -5.5f, 3.5f },
                            { 32, -4.5f, 3.5f },
                            { 45, -5.7f, 4 }
                          };

        //ステージ２(引き寄せられる)
        float[,] Stage2 = { { -1, 18, 3 },
                            { -1, 22.25f, 3.7f },
                            { 11.5f, 25.5f, 2.5f },
                            { 11.5f, 19, 2.5f },
                            { 11.5f, 12.5f, 2.5f },
                            { 22, 12.5f, 3 },
                            { 23, 19.5f, 4 },
                            { 28, 23, 2.5f },
                            { 37, 23.5f, 3 },
                            { 38.5f, 18.5f, 4 },
                            { 43, 24.5f, 2 },
                            { 41, 30, 3.5f },
                            { 41, 39, 3.5f },
                            { 41, 47, 3.5f },
                            { 28, 47, 3.5f },
                            { 18.5f, 43, 4.3f },
                            { 2.5f, 46, 4 },
                            { -14.5f, 43.5f, 4 }
                          };

        //上記配列の入れ箱　初期化関数呼び出し時、ステージ番号によって入れる配列が変化
        float[,] RoomData;

        //現在の部屋番号
        int CurrentRoom = 0;

        //ステージ番号
        [SerializeField]
        int CurrentStage = 0;

        //カメラ
        Camera m_camera;

        // Use this for initialization
        void Start()
        {
            m_camera = Camera.main;

            switch (CurrentStage)
            {
                case 0:
                    RoomData = Stage1;
                    break;

                case 1:
                    RoomData = Stage2;
                    break;

            }

            //最初の部屋にセットやで
            SetCamera(CurrentRoom);

        }

        // Update is called once per frame
        void Update()
        {

        }

        //カメラをセット
        void SetCamera(int arg_roomNum)
        {
            m_camera.transform.position = new Vector3(RoomData[arg_roomNum, 0], RoomData[arg_roomNum, 1], -10);
            m_camera.orthographicSize = RoomData[arg_roomNum, 2];

            //レイ飛ばしてギミック始動させたい
        }

        //コライダーから呼ばれる現在どのルームに触れているかの関数
        public void SetCurrentRoom(int arg_count)
        {
            //前回呼ばれた時と別の数字なら実行
            if (CurrentRoom != arg_count)
            {
                CurrentRoom = arg_count;

                //スムーズなカメラ遷移、スクリプトの都合上部屋番号＋１が引数
                CameraPosController.Instance.SetTargetAndStart(CurrentRoom+1);

                //ToDo：SetCamera関数を使わなくなったことによるギミック作動方法の思案
                //SetCamera(CurrentRoom);
            }
        }
    }

}