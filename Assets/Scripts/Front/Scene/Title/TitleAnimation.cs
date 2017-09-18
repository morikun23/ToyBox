using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Title
{
    public class TitleAnimation : MonoBehaviour
    {
        Vector3 m_move = new Vector3(0.05f, 0, 0);
        Vector3 m_reStart = new Vector3(-15f, -3.85f, 0);
        Animator m_animator;
        SpriteRenderer m_renderer;

        [SerializeField]
        GameObject m_spComment;

        TitleComment m_titleComment;


        bool m_stop = true;

        // Use this for initialization
        void Start()
        {
            m_animator = GetComponent<Animator>();
            m_renderer = GetComponent<SpriteRenderer>();
            m_titleComment = m_spComment.GetComponent<TitleComment>();
            StartCoroutine("Wait");
            
        }

        private IEnumerator Wait()
        {

            while (true)
            {

                if (transform.position.x >= 5.5f && m_stop)
                {
                    m_renderer.flipX = false;
                    m_animator.SetBool("RunFlag", false);
                    m_animator.SetBool("IdleFlag", true);
                    yield return new WaitForSeconds(3.0f);
                    m_titleComment.Spawn();
                    yield return new WaitForSeconds(1.0f);
                    m_titleComment.Delete();

                    m_stop = false;
                }
                else
                {
                    m_renderer.flipX = true;
                    m_animator.SetBool("RunFlag", true);
                    m_animator.SetBool("IdleFlag", false);
                    transform.position += m_move;
                }

                if (transform.position.x >= 10f)
                {
                    transform.position = m_reStart;
                    m_stop = true;
                }


                yield return null;

            }
        }
    }

}
