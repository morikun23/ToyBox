using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ToyBox
{
    public class Press : MonoBehaviour
    {
        Rigidbody2D rigidbody2d;
        RectTransform m_rectTransform;
        BoxCollider2D m_collider;

        Vector2 m_startCollider;
        Vector3 m_startSize;
        Vector3 m_startPosition;
        Vector3 m_thornPosition = new Vector3 (0,-1f);

        [SerializeField]
        float startTime;

        [SerializeField]
        float speed;

        float accele;
        float inertia = 1.15f;

        [SerializeField]
        float derayTime;

        bool reset = false;

        void Start()
        {
            Initialize();
            StartCoroutine("UpdateByFrame");
        }

        public void Initialize()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            m_rectTransform = GetComponent<RectTransform>();
            m_collider = GetComponent<BoxCollider2D>();

            m_startPosition = m_rectTransform.position;
            m_startSize = m_rectTransform.sizeDelta;
            m_startCollider = m_collider.size;

            accele = speed;
        }

        public IEnumerator UpdateByFrame()
        {
            yield return new WaitForSeconds(startTime);

            while (true)
            {
                if (reset)
                {
                    m_collider.size += new Vector2(0, -speed * inertia);
                    m_rectTransform.position += new Vector3(0, speed * inertia/ 2);
                    m_rectTransform.sizeDelta += new Vector2(0, -speed * inertia);
                    m_thornPosition += new Vector3(0, speed * inertia/2);



                    if (m_startPosition.y < m_rectTransform.position.y)
                    {
                        m_rectTransform.position = m_startPosition;
                        m_rectTransform.sizeDelta = m_startSize;
                        m_collider.size = m_startCollider;
                        reset = false;
                        yield return new WaitForSeconds(derayTime);
                    }

                }
                else
                {
                    m_collider.size += new Vector2(0, accele);
                    m_rectTransform.position += new Vector3(0, -accele / 2);
                    m_rectTransform.sizeDelta += new Vector2(0, accele);
                    m_thornPosition += new Vector3(0, -accele / 2);
                    accele = accele * inertia;
                
                    RaycastHit2D hitGround = Physics2D.BoxCast(transform.position + new Vector3(0f, -4f), new Vector2(1f, 3f), 1f, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("Ground"));
                    if (hitGround)
                    {
                        reset = true;
                        yield return new WaitForSeconds(1f);
                        accele = speed;
                    }

                }
                RaycastHit2D hitPlayer = Physics2D.BoxCast(transform.position + m_thornPosition, new Vector2(3f, 1.5f), 1f, Vector2.zero, 0.1f, 1 << LayerMask.NameToLayer("Player"));
                if(hitPlayer)
                {
                    Hit();
                }
                yield return null;
            }

        }

        //void OnDrawGizmos()
        //{
        //    Gizmos.DrawWireCube(transform.position + m_thornPosition, new Vector2(3f, 1.5f));
        //}
        public void Hit()
        {
            Debug.Log("hitPlayer");
            //プレイヤーに当たった時の処理はここにお願いします
        }
        
    }
}
