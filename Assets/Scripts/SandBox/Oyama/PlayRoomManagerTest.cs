using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Oyama
{

    public class PlayRoomManagerTest : MonoBehaviour
    {

        private static PlayRoomManagerTest m_instance = null;
        public static PlayRoomManagerTest Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new GameObject("PlayRoomManagerTest").AddComponent<PlayRoomManagerTest>();
                }
                return m_instance;
            }
        }

        //ギミックのリスト予定
        //List<Gimmick> 的な

        //部屋ごとのカメラの座標、Y座標、カメラのSize(どれだけズームするか)を集めた配列
        float[,] CameraSet = { { -51, -6, 4 },
                               { -35, -6, 4 },
                               { -20, -5.5f, 3 },
                               { -11, -3.5f, 4 },
                               { -3, -2.5f, 3.2f },
                               { 10.5f, -1.5f, 4.2f },
                               { 21.5f, -5.5f, 3.5f },
                               { 32, -4.5f, 3.5f },
                               { 45, -5.7f, 4 }
                             };

        //現在の部屋番号
        int CurrentRoom = 0;

        //カメラ
        Camera camera;

        // Use this for initialization
        void Start()
        {
            camera = Camera.main;

            //最初の部屋にセットやで
            SetCamera(CameraSet[CurrentRoom, 0], CameraSet[CurrentRoom, 1], CameraSet[CurrentRoom, 2]);

        }

        // Update is called once per frame
        void Update()
        {

        }

        //カメラをセット
        void SetCamera(float arg_x,float arg_y,float arg_size)
        {
            camera.transform.position = new Vector3(arg_x, arg_y, -10);
            camera.orthographicSize = arg_size;

            //レイ飛ばしてギミック始動させたい
        }

        //コライダーから呼ばれる現在どのルームに触れているかの関数
        public void SetCurrentRoom(int arg_count)
        {
            //前回呼ばれた時と別の数字なら実行
            if (CurrentRoom != arg_count)
            {
                CurrentRoom = arg_count;

                SetCamera(CameraSet[CurrentRoom, 0], CameraSet[CurrentRoom, 1], CameraSet[CurrentRoom, 2]);
            }
        }
    }

}