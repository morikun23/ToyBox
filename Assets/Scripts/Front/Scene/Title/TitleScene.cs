using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Title {
	public class TitleScene : ToyBox.Scene {

        //「タッチでスタート！！」
        [SerializeField]
        GameObject m_startMessage;
        public SpriteRenderer m_startMessageRenderer;

        //フェード用
        [SerializeField]
        GameObject m_filter;
        public SpriteRenderer m_filterRenderer;

        float m_alphaAdd = 0.015f;

        public AudioClip m_sound;

        [SerializeField]
        GameObject m_gear1, m_gear2, m_gear3;

        GearRotate m_gear1Script, m_gear2Script, m_gear3Script;

		public override IEnumerator OnEnter() {
            //base.OnEnter();

            m_startMessageRenderer = m_startMessage.GetComponent<SpriteRenderer>();

            m_filterRenderer = m_filter.GetComponent<SpriteRenderer>();

            m_sound = (AudioClip)Resources.Load("Audio/SE/SE_TitleTouch");

            m_filterRenderer.color = new Color(1, 1, 1, 0);

            m_gear1Script = m_gear1.GetComponent<GearRotate>();
            m_gear1Script.Init(1);
            m_gear2Script = m_gear2.GetComponent<GearRotate>();
            m_gear2Script.Init(-0.5f);
            m_gear3Script = m_gear3.GetComponent<GearRotate>();
            m_gear3Script.Init(2.5f);

            //yield return null ;
            AppManager.Instance.m_fade.StartFade(new FadeIn(), Color.black, 1.0f);
            yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);

        }

		public override IEnumerator OnUpdate() {
            //base.OnUpdate();
            while (true)
            {

                m_gear1Script.OnUpdate();
                m_gear2Script.OnUpdate();
                m_gear3Script.OnUpdate();

                //タッチでスタート！！の点滅
                m_startMessageRenderer.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));

                //if (Input.GetTouch(0).phase == TouchPhase.Began)
                if (Input.GetMouseButtonDown(0))
                {
                    AudioSource.PlayClipAtPoint(m_sound, Camera.main.gameObject.transform.position);
                    break;
                }

                yield return null;
            }

        }

		public override IEnumerator OnExit() {
            //base.OnExit();
            AppManager.Instance.m_fade.StartFade(new FadeOut(), Color.black, 1.0f);
            yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
            yield return null;
        }
	}
}