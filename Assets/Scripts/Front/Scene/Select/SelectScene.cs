using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class SelectScene : ToyBox.Scene
    {

        [SerializeField]
		Sprite NoButtonPress, YesButtonPress,Stage1ButtonPress,Stage2ButtonPress;

        int selectResult;

        public override IEnumerator OnEnter()
        {
			selectResult = 0;
            AppManager.Instance.m_fade.StartFade(new FadeIn(), Color.white, 1.0f);
            yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
        }

        public override IEnumerator OnUpdate()
        {
            

            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    //タッチをした位置にオブジェクトがあるかどうかを判定
                    RaycastHit2D Hit = Physics2D.Raycast(worldPoint, Vector2.zero, 0.1f);

                    if (Hit)
                    {
                        if(Hit.collider.gameObject.name == "UI_Button_YES")
                        {
                            Hit.collider.GetComponent<SpriteRenderer>().sprite = YesButtonPress;
                            AppManager.Instance.m_audioManager.CreateSe("SE_TitleTouch").Play();
                            selectResult = 1;

                            break;
                        }
                        else if (Hit.collider.gameObject.name == "UI_Button_NO")
                        {
                            Hit.collider.GetComponent<SpriteRenderer>().sprite = NoButtonPress;
                            AppManager.Instance.m_audioManager.CreateSe("SE_TitleTouch").Play();
                            selectResult = 2;

                            break;
                        }
						else if(Hit.collider.gameObject.name == "SP_SelectStage1")
						{
							Hit.collider.GetComponent<SpriteRenderer>().sprite = Stage1ButtonPress;
							AppManager.Instance.m_audioManager.CreateSe("SE_TitleTouch").Play();
							selectResult = 3;

							break;
						}
						else if (Hit.collider.gameObject.name == "SP_SelectStage2")
						{
							Hit.collider.GetComponent<SpriteRenderer>().sprite = Stage2ButtonPress;
							AppManager.Instance.m_audioManager.CreateSe("SE_TitleTouch").Play();
							selectResult = 4;

							break;
						}
                    }

                }
                yield return null;
            }
        }

        public override IEnumerator OnExit()
        {

            AppManager.Instance.m_fade.StartFade(new FadeOut(), Color.black, 2.0f);
            yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);

            //次へ進む
            if (selectResult == 1)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
            }
            //進まない
            else if (selectResult == 2) {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
            }
			//ステージ１をえらんだ
			else if (selectResult == 3)
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
			}
			//ステージ２を選んだ
			else if (selectResult == 4) {
				UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
			}

        }
    }

}