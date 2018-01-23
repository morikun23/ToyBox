using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ToyBox
{
    //ステージ選択ボタンが押された時に扱いたい変数を格納したクラス
    [System.Serializable]
    public class StageSelectButtonInfo
    {
        public int m_stageNumber;
        public string m_stageName;
        public Sprite m_screenSprite;
    }

    public class StageSelectScene : ToyBox.Scene
    {
        [SerializeField]
        private StageSelectInfo m_stageSelectInfo;
        
        [SerializeField]
        private BackGroundMover m_backGroundMover;

        [SerializeField]
        private GameObject m_stageSelectScrollView;

        [SerializeField]
        private GameObject m_halfPointSelectScrollView;

        [SerializeField]
        private GameObject m_frontButtonLayer;

        [SerializeField]
        private StageSelectButtonInfo[] m_stageSelectButtonInfo;

        [SerializeField]
        private Button[] m_stageSelectButton;

        [SerializeField]
        private Image m_screenImage;
        private Sprite m_screenBase;

        //中間地点を選ぶボタン郡の親になるオブジェ
        //m_halfPointSelectScrollView内のContentというオブジェがこれに当たる
        private GameObject m_halfPointContent;

        enum SelectSceneState
        {
            StageSelect,
            BackGroundMoving,
            ChangingView,
            SelectHalfPoint
        }

        private SelectSceneState m_state;

        private bool m_isChangingScrollView;

        //ボタンを入れ替えた時のステートを保存
        private SelectSceneState m_whenChangesState;

        //中間地点のバッファ
        private int m_halfPointNumber;

        //各ボタンの制御用
        private bool m_isStageSelect;
        private bool m_isBack;

        //ボタンのSprite
        private Sprite m_trueSprite;
        private Sprite m_falseSprite;

        //モーダルで表示するメッセージ
        private string m_modalMessage;

        //シーン遷移を判断するstring
        private string m_sceneString;

        public override IEnumerator OnEnter()
        {
            //ステージの踏破具合などを取得
            m_stageSelectInfo.Initialize();
            
            //背景動かしさんを初期化
            m_backGroundMover.Initialize();

            //最初のステートはステージを選ぶ段階
            m_state = SelectSceneState.StageSelect;

            m_halfPointContent = m_halfPointSelectScrollView.transform.Find("Viewport/Content").gameObject;
            
            m_isChangingScrollView = false;

            m_trueSprite = Resources.Load<Sprite>("Contents/StageSelect/Textures/SP_HalfPointButton_Normal");

            m_falseSprite = Resources.Load<Sprite>("Contents/StageSelect/Textures/SP_HalfPointButton_False");

            //スクリーンで最初に使われている画像を取得
            m_screenBase = m_screenImage.sprite;

            //ステージ選択ボタンの初期化
            for (int i = 0; i < m_stageSelectButton.Length; i++)
            {
                StageSelectButtonInfo temp = new StageSelectButtonInfo();

                temp = m_stageSelectButtonInfo[i];

                m_stageSelectButton[i].onClick.AddListener(() => { OnStageSelectedPress(temp); });

                //到達率をInfoから取得
                int arrivalCount = m_stageSelectInfo.GetOpenStageCount();

                Image image = m_stageSelectButton[i].GetComponent<Image>();

                //生成されたのが一個目のボタンなら
                if (i == 0)
                {
                    image.sprite = m_trueSprite;

                }
                //生成したボタンが到達率より低いボタンなら
                else if (i < arrivalCount)
                {
                    image.sprite = m_trueSprite;

                }
                //生成したボタンが到達率より高いボタンなら
                else
                {

                    //ボタンのグラフィックを灰色の物に差し替え、Buttonを切る
                    image.sprite = m_falseSprite;
                    m_stageSelectButton[i].GetComponent<Button>().enabled = false;
                }

            }

            m_sceneString = "";

            m_stageSelectScrollView.SetActive(false);
            m_stageSelectScrollView.SetActive(true);

            AudioManager.Instance.RegisterBGM("BGM_StageSelect");
            AudioManager.Instance.PlayBGM(1.5f);

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

                        //ステージが選ばれたら遷移
                        if (m_isStageSelect)
                        {
                            m_backGroundMover.FocusAtPoint(AppManager.Instance.user.m_temp.m_playStageId);
                            CreateHalfPointButtons(AppManager.Instance.user.m_temp.m_playStageId);

                            //戻るボタン、ステージセレクトボタンを一時的に押せなくする
                            ChangeButtonActive(false);

                            m_state = SelectSceneState.BackGroundMoving;

                            m_isStageSelect = false;
                        }

                        //戻るボタンが押されたら
                        if (m_isBack)
                        {

                            //タイトルシーンへ戻りますか？

                            m_isBack = false;

                            //モーダルの表示
                            UIManager.Instance.PopupGameReadyModal("タイトルへ戻りますか？", () => {
                                AudioManager.Instance.QuickPlaySE("SE_StageSelectButton");
                                m_sceneString = "Title";
                            });
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
                            ChangeButtonActive(true);

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
                        
                        //戻るボタンが押されたら遷移
                        if (m_isBack)
                        {

                            //戻るボタン、ステージセレクトボタンを一時的に押せなくする
                            ChangeButtonActive(false);

                            //スクロールを入れ変える
                            ChangingScrollView(m_stageSelectScrollView, m_halfPointSelectScrollView);
                            m_state = SelectSceneState.ChangingView;

                            //スクリーン画像を最初の画像に戻す
                            m_screenImage.sprite = m_screenBase;

                            m_isBack = false;
                        }

                        break;


                }

                //遷移するシーンが決まったら遷移する
                if (m_sceneString == "Main" || m_sceneString == "Title") yield break;
                yield return null;

            }

        }

        public override IEnumerator OnExit()
        {
            
            AppManager.Instance.m_fade.StartFade(new FadeOut(), Color.black, 1.0f);
            yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
            AudioManager.Instance.StopBGM();
            SceneManager.LoadScene(m_sceneString);
        }


        /// <summary>
        ///　スクロールビューを入れ替える
        /// </summary>
        /// <param name="arg_view1">一つめのViewオブジェ</param>
        /// <param name="arg_view2">二つ目のViewオブジェ</param>
        private void ChangingScrollView(GameObject arg_view1, GameObject arg_view2)
        {
            //この関数が呼ばれた時のステートを取得しておく
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

            //到達率をInfoから取得
            int arrivalCount = m_stageSelectInfo.GetArrivalRoomCount(stageNumber);

            for (int i = 1; i <= m_stageSelectInfo.GetTotalRoomCount(stageNumber); i++)
            {
                GameObject button = Instantiate(buttonPrefab, m_halfPointContent.transform);
                button.name = button.transform.Find("Text").GetComponent<Text>().text = i.ToString();//見栄え上、名前は０からでなく１からにする

                //OnClickへの関数の登録をfor文の中で行う場合、変数を一時的に保存しとく必要がある
                int temp = i;

                button.GetComponent<Button>().onClick.AddListener(() => { OnHalfPointSelectedPress(temp); });

                Image image = button.GetComponent<Image>();

                //最初の小部屋は解放
                if (i == 1)
                {
                    image.sprite = m_trueSprite;

                }
                //生成したボタンが到達率より低いボタンなら
                else if (i < arrivalCount)
                {
                    image.sprite = m_trueSprite;

                }
                //生成したボタンが到達率より高いボタンなら
                else
                {
                    
                    //ボタンのグラフィックを灰色の物に差し替え、Buttonを切る
                    image.sprite = m_falseSprite;
                    button.GetComponent<Button>().enabled = false;
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

        /// <summary>
        /// 戻るボタンを押した処理
        /// </summary>
        public void OnBackSelectedPress()
        {
            AudioManager.Instance.QuickPlaySE("SE_StageSelectButton");
            m_isBack = true;
        }

        /// <summary>
        /// ステージを選択するボタンを押した処理
        /// </summary>
        /// <param name="arg_info">ボタンを押した際の情報の集まり</param>
        public void OnStageSelectedPress(object arg_info)
        {
            AudioManager.Instance.QuickPlaySE("SE_StageSelectButton");

            StageSelectButtonInfo info = arg_info as StageSelectButtonInfo;

            uint id = uint.Parse(info.m_stageNumber.ToString());

            //ステージの決定
            AppManager.Instance.user.m_temp.m_playStageId = id;

            //画像の変更
            m_screenImage.sprite = info.m_screenSprite;

            //モーダルで表示するステージ名の変更
            m_modalMessage = info.m_stageName + "\n";

            m_isStageSelect = true;

        }

        /// <summary>
        /// 中間地点を選択するボタンを押した処理
        /// </summary>
        /// <param name="arg_number">どの中間地点か</param>
        public void OnHalfPointSelectedPress(int arg_number)
        {
            AudioManager.Instance.QuickPlaySE("SE_StageSelectButton");

            uint id = uint.Parse(arg_number.ToString());

            //中間地点の決定
            AppManager.Instance.user.m_temp.m_playRoomId = id;
            
            //モーダルで表示するステージ名の変更
            //すでに一度文字列の変更が加わっているのなら、処理しない
            if(m_modalMessage.IndexOf("番目の小部屋でよろしいですか？") == -1)
            m_modalMessage = m_modalMessage + "の\n" + id + "番目の小部屋でよろしいですか？";

            //モーダルの表示
            UIManager.Instance.PopupGameReadyModal(m_modalMessage,() => {
                AudioManager.Instance.QuickPlaySE("SE_OnTransitionMain");
                m_sceneString = "Main";
            });

        }

        /// <summary>
        /// frontLayer内のButtonスクリプトの無効・有効を切り替える
        /// </summary>
        /// <param name="arg_stageNumber">無効か有効か</param>
        public void ChangeButtonActive(bool arg_active)
        {
            foreach(Transform btn in m_frontButtonLayer.transform)
            {
                if(btn.GetComponent<Button>())
                btn.GetComponent<Button>().enabled = arg_active;
            }

        }

    }
}