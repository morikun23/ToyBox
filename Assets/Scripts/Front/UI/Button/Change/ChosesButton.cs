using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToyBox
{
    public class ChosesButton : MobileInput
    {

        [SerializeField]
        GameObject m_chosesObject;
        Choses m_chosesScript;

        [SerializeField]
        private ChosesBase.YorN m_type;

        Animator m_animator;

        void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            m_chosesScript = m_chosesObject.GetComponent<Choses>();
            m_animator = GetComponent<Animator>();
        }

        public override void Started()
        {
            base.Started();
        }

        public override void TouchEnd()
        {
            base.TouchEnd();
            OnUp();

        }

        public override void SwipeEnd()
        {
            base.SwipeEnd();
            OnUp();
        }


        void OnUp()
        {
            m_chosesScript.answer = m_type;
        }

        void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //タッチをした位置にオブジェクトがあるかどうかを判定
                RaycastHit2D NoHit = Physics2D.Raycast(worldPoint, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("NoButton"));
                RaycastHit2D YesHit = Physics2D.Raycast(worldPoint, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("YesButton"));

                if (NoHit)
                {
                    m_chosesScript.answer = ChosesBase.YorN.NO;
                    m_animator.SetBool("NoFlg", true);
                }
                else if(YesHit)
                {
                    m_chosesScript.answer = ChosesBase.YorN.YES;
                    m_animator.SetBool("YesFlg", true);

                }
            }

        }
    }
}

