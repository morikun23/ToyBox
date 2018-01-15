using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ToyBox
{

    public class StageSelectScene : ToyBox.Scene
    {
        [SerializeField]
        private StageSelectInfo m_stageSelectInfo;

        [SerializeField]
        StageSelectSceneUI m_stageSelectSceneUI;

        [SerializeField]
        private BackGroundMover m_backGroundMover;

        [SerializeField]
        private GameObject m_stageSelectScrollView;

        [SerializeField]
        private GameObject m_halfPointSelectScrollView;

        //中間地点を選ぶボタン郡の親になるオブジェ
        //m_halfPointSelectScrollView内のContentというオブジェがこれに当たる
        private GameObject m_halfPointContent;

        enum SelectSceneState
        {
            StageSelect,
            BackGroundMoving,
            ChangingView,
            SelectHalfPoint,
            ModalIndicate
        }

        private SelectSceneState m_state;

        private bool m_isChangingScrollView;
        private SelectSceneState m_whenChangesState;

        public override IEnumerator OnEnter()
        {
            //ステージの踏破具合などを取得
            m_stageSelectInfo.Initialize();

            //ボタン達の初期化
            m_stageSelectSceneUI.Initialize();

            //背景動かしさんを初期化
            m_backGroundMover.Initialize();

            //最初のステートはステージを選ぶ段階
            m_state = SelectSceneState.StageSelect;

            m_halfPointContent = m_halfPointSelectScrollView.transform.Find("Viewport/Content").gameObject;
            
            m_isChangingScrollView = false;

            AppManager.Instance.m_fade.StartFade(new FadeIn(), Color.black, 1.0f);
            yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);

        }

        public override IEnumerator OnUpdate()
        {
            while (true)
            {
                switch (m_state)
                {
                    //ステージを選ぶ
                    case SelectSceneState.StageSelect:

                        if (m_stageSelectSceneUI.IsStageSelected())
                        {
                            m_backGroundMover.FocusAtPoint(AppManager.Instance.user.m_temp.m_playStageId);
                            CreateHalfPointButtons(AppManager.Instance.user.m_temp.m_playStageId);

                            //戻るボタン、ステージセレクトボタンを一時的に押せなくする
                            m_stageSelectSceneUI.SetInputEnable(LayerType.Front, false);

                            m_state = SelectSceneState.BackGroundMoving;
                        }

                        //戻るボタンが押されたら
                        if (m_stageSelectSceneUI.IsBackButtonSelected())
                        {
                            //タイトルシーンへ戻りますか？
                        }

                        break;

                    //背景が動いている
                    case SelectSceneState.BackGroundMoving:

                        //動きが止まったら遷移
                        if (!m_backGroundMover.IsFocusing())
                        {
                            ChangingScrollView(m_stageSelectScrollView, m_halfPointSelectScrollView);
                            m_state = SelectSceneState.ChangingView;

                        }
                        break;

                    //スクロールビューを入れ替えている
                    case SelectSceneState.ChangingView:

                        //入れ替えが終了したら遷移
                        if (!m_isChangingScrollView)
                        {
                            //レイヤーの押せなくなっている状態を解除
                            m_stageSelectSceneUI.SetInputEnable(LayerType.Front, true);

                            //BackGroundMovingから呼ばれたのなら、中間地点を選ぶステートへ
                            if (m_whenChangesState == SelectSceneState.BackGroundMoving)
                            {
                                m_state = SelectSceneState.SelectHalfPoint;
                            }
                            //SelectHalfPointから呼ばれたのなら、ステージを選ぶステートへ
                            else if (m_whenChangesState == SelectSceneState.SelectHalfPoint)
                            {
                                //生成されていたボタンを削除
                                DestroyHalfPointButtons();
                                m_state = SelectSceneState.StageSelect;
                            }
                        }

                        break;

                    //中間地点を選ぶ
                    case SelectSceneState.SelectHalfPoint:

                        if (m_stageSelectSceneUI.IsBackButtonSelected())
                        {
                            //戻るボタン、ステージセレクトボタンを一時的に押せなくする
                            m_stageSelectSceneUI.SetInputEnable(LayerType.Front, false);

                            //スクロールを入れ変える
                            ChangingScrollView(m_stageSelectScrollView, m_halfPointSelectScrollView);
                            m_state = SelectSceneState.ChangingView;
                        }

                        break;

                    //モーダルによるはい・いいえ
                    case SelectSceneState.ModalIndicate:

                        //はいが選ばれたらメインシーンに遷移したい。。。

                        break;

                }

                yield return null;

            }

        }


        public override IEnumerator OnExit()
        {

            AppManager.Instance.m_fade.StartFade(new FadeOut(), Color.black, 1.0f);
            yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
            SceneManager.LoadScene("Main");
        }


        /// <summary>
        ///　スクロールビューを入れ替える
        /// </summary>
        /// <param name="arg_view1">一つめのViewオブジェ</param>
        /// <param name="arg_view2">二つ目のViewオブジェ</param>
        private void ChangingScrollView(GameObject arg_view1, GameObject arg_view2)
        {
            //入れ替え関数が呼ばれた時のステートを取得しておく
            m_whenChangesState = m_state;
            
            m_isChangingScrollView = true;

            Vector3 pos1 = arg_view1.transform.localPosition;
            Vector3 pos2 = arg_view2.transform.localPosition;

            Hashtable hashtable1 = new Hashtable();
            hashtable1.Add("position", pos2);
            hashtable1.Add("islocal", true);//ローカル座標に変換
            hashtable1.Add("time", 1);//Tweenの時間

            iTween.MoveTo(arg_view1, hashtable1);

            Hashtable hashtable2 = new Hashtable();
            hashtable2.Add("position", pos1);
            hashtable2.Add("islocal", true);//ローカル座標に変換
            hashtable2.Add("time", 1);//Tweenの時間
            hashtable2.Add("oncomplete", "ChangingEnd");//Tween終了時に呼ばれる処理
            hashtable2.Add("oncompletetarget", gameObject);//↑の処理を持っているオブジェクトを明記

            iTween.MoveTo(arg_view2, hashtable2);
        }

        private void ChangingEnd()
        {
            m_isChangingScrollView = false;
        }

        /// <summary>
        /// 引数の番号のステージの小部屋のボタン郡を生成
        /// </summary>
        /// <param name="arg_stageNumber">どのステージの</param>
        private void CreateHalfPointButtons(uint arg_stageNumber)
        {
            //値を変換
            int stageNumber = int.Parse((arg_stageNumber / 1000).ToString());

            //ボタンの元になるPrefab
            GameObject buttonPrefab = Resources.Load<GameObject>("Contents/StageSelect/Prefabs/PF_HalfPointSelectButton");

            //ボタンのSprite
            Sprite trueSprite = Resources.Load<Sprite>("Contents/StageSelect/Textures/SP_HalfPointButton_Normal");
            Sprite falseSprite = Resources.Load<Sprite>("Contents/StageSelect/Textures/SP_HalfPointButton_False");

            //到達率をInfoから取得
            int arrivalCount = m_stageSelectInfo.GetArrivalRoomCount(stageNumber);

            for (int i = 0; i < m_stageSelectInfo.GetTotalRoomCount(stageNumber); i++)
            {
                GameObject button = Instantiate(buttonPrefab, m_halfPointContent.transform);
                button.name = button.transform.Find("Text").GetComponent<Text>().text = (i + 1).ToString();//見栄え上、名前は０からでなく１からにする

                Image image = button.GetComponent<Image>();

                //生成されたのが一個目のボタンなら
                if (i == 0)
                {
                    image.sprite = trueSprite;

                }
                //生成したボタンが到達率より低いボタンなら
                else if (i < arrivalCount)
                {
                    image.sprite = trueSprite;

                }
                //生成したボタンが到達率より高いボタンなら
                else
                {
                    
                    //ボタンのグラフィックを灰色の物に差し替え、TouchActorを切る
                    image.sprite = falseSprite;
                    button.GetComponent<TouchActor>().enabled = false;
                }
            }

        }

        /// <summary>
        /// 生成している中間地点を選ぶボタンを削除
        /// </summary>
        private void DestroyHalfPointButtons()
        {
            for (int i = 0; i < m_halfPointContent.transform.childCount; ++i)
            {
                Destroy(m_halfPointContent.transform.GetChild(i).gameObject);
            }
        }

    }
}