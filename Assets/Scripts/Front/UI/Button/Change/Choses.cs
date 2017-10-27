using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class Choses : MonoBehaviour
    {
        [SerializeField]
        GameObject m_playArea;
        [SerializeField]
        GameObject m_yes;
        [SerializeField]
        GameObject m_no;
        [SerializeField]
        GameObject m_clear;

        [SerializeField]
        GameObject m_diaPrefab;

        public ChosesBase.YorN answer = 0;


        public void Create()
        {
            transform.position = m_playArea.transform.position;
            transform.position += new Vector3(0, 0, 5);
            StartCoroutine("UpdateByFrame");

        }

        IEnumerator UpdateByFrame()
        {
            Instantiate(m_diaPrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(3f);
            
            m_yes.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            m_no.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            m_clear.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);

            while (true)
            {       
                if(answer == ChosesBase.YorN.NO)
                {
                    
                    AppManager.Instance.m_fade.StartFade(new FadeOut(), Color.white, 3f);
                    yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
                    Application.LoadLevel("Scenes/Main/Title");
                }
                else if(answer == ChosesBase.YorN.YES)
                {

                    AppManager.Instance.m_fade.StartFade(new FadeOut(), Color.white, 3f);
                    yield return new WaitWhile(AppManager.Instance.m_fade.IsFading);
                    Application.LoadLevel("Scenes/Main/Main");
                }

                yield return null;
            }
        }

    }
}