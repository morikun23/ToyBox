using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class SceneChange : MonoBehaviour
    {

        [SerializeField]
        GameObject m_chose;

        public SpriteRenderer m_stageFilter;
        public SpriteRenderer m_uiFilter;

        float alpha = 0.015f;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine("StageEnd");
            }
        }

        // 呼び出すだけでフィルターがかかります
        public IEnumerator StageEnd()
        {
            while (true)
            {
                
                m_uiFilter.color += new Color(0, 0, 0, alpha);
                m_stageFilter.color += new Color(0, 0, 0, alpha);

                if (m_uiFilter.color.a >= 1)
                {
                    m_chose.GetComponent<Chosies>().Create();
                    break;
                }
                yield return null;
            }

        }

    }
}