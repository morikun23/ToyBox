using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Title {
	public class TitleScene : Scene {
        private static TitleScene m_instance;

        public static TitleScene Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new GameObject("TitleScene").AddComponent<TitleScene>();
                }
                return m_instance;
            }
        }

        //「タッチでスタート！！」
        GameObject m_startMessage;
        public SpriteRenderer m_startMessageRenderer;

        //フェード用
        GameObject m_filter;
        public SpriteRenderer m_filterRenderer;
        float alphaAdd = 0.015f;


        bool next;

        public AudioClip sound;

        GearRotate gear1, gear2, gear3;

        public TitleScene() : base("Scene/TitleScene") {

		}

		public override void OnEnter() {
			base.OnEnter();

            m_startMessage = GameObject.Find("SP_StartMessage");
            m_startMessageRenderer = m_startMessage.GetComponent<SpriteRenderer>();

            m_filter = GameObject.Find("SP_Filter");
            m_filterRenderer = m_filter.GetComponent<SpriteRenderer>();

            sound = (AudioClip)Resources.Load("Sounds/SE/SE_TitleTouch");

            m_filterRenderer.color = new Color(1, 1, 1, 0);
            next = false;

            gear1 = GameObject.Find("SP_Logo/SP_LogoGear1").GetComponent<GearRotate>();
            gear1.Init(1);

            

            gear2 = GameObject.Find("SP_Logo/SP_LogoGear2").GetComponent<GearRotate>();
            gear2.Init(-0.5f);
            gear3 = GameObject.Find("SP_Logo/SP_LogoGear3").GetComponent<GearRotate>();
            gear3.Init(2.5f);

        }

		public override void OnUpdate() {
			base.OnUpdate();


            gear1.OnUpdate();
            gear2.OnUpdate();
            gear3.OnUpdate();

            //タッチでスタート！！の点滅
            m_startMessageRenderer.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));

            if (next) m_filterRenderer.color += new Color(0, 0, 0, alphaAdd);

            if (m_filterRenderer.color.a > 1)
            {
                Application.LoadLevel(1);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioSource.PlayClipAtPoint(sound, Camera.main.gameObject.transform.position);
                next = true;
            }
            if (Input.touchCount < 1) return;
            if (Input.GetTouch(0).phase == TouchPhase.Began && next == false)
            {
                //シーン移動したい
                AudioSource.PlayClipAtPoint(sound, Camera.main.gameObject.transform.position);
                next = true;
                Debug.Log("in");
            }

        }

		public override void OnExit() {
			base.OnExit();
		}
	}
}