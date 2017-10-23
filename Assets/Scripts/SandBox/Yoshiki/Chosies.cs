using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class Chosies : MonoBehaviour
    {
        [SerializeField]
        GameObject m_playArea;
        [SerializeField]
        GameObject m_yes;
        [SerializeField]
        GameObject m_no;
        [SerializeField]
        GameObject m_or;

        public int answer = 0;


        public void Create()
        {
            transform.position = m_playArea.transform.position;
            transform.position += new Vector3(0, 0, 5);
            m_yes.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 255);
            m_no.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 255);
            m_or.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 255);

            StartCoroutine("UpdateByFrame");

        }

        IEnumerator UpdateByFrame()
        {
            while(true)
            {       
                if(answer == 1)
                {
                    Application.LoadLevel("Yoshiki2");
                }
                else if(answer == -1)
                {
                    Application.LoadLevel("Yoshiki");
                }

                //AppManager.Instance.m_fade.StartFade(new FadeOut(), Color.black, 0.5f);
                //yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
                
                yield return null;
            }
        }

    }
}