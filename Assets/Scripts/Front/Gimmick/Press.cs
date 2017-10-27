using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ToyBox
{
    public class Press : MonoBehaviour
    {
        RectTransform m_rectTransform;
        BoxCollider2D m_collider;

        Vector2 m_startCollider;
        Vector3 m_startSize;
        Vector3 m_startPosition;
        Vector3 m_thornPosition = new Vector3 (0,-1f);

        [SerializeField]
        float m_startTime;

        [SerializeField]
        float m_speed;

        float m_accele;
        float INERTIA = 1.15f;

        [SerializeField]
        float m_derayTime;

        bool m_hited = false;
        bool m_reset = false;

        void Start()
        {
            Initialize();
        }

		void OnEnable() {
			StartCoroutine(UpdateByFrame());
		}

        public void Initialize()
        {
            m_rectTransform = GetComponent<RectTransform>();
            m_collider = GetComponent<BoxCollider2D>();

            m_startPosition = m_rectTransform.position;
            m_startSize = m_rectTransform.sizeDelta;
            m_startCollider = m_collider.size;

            m_accele = m_speed;
        }

        public IEnumerator UpdateByFrame()
        {
            yield return new WaitForSeconds(m_startTime);

            while (true)
            {
                if (m_reset)
                {
                    m_collider.size += new Vector2(0, -m_speed * INERTIA);
                    m_rectTransform.position += new Vector3(0, m_speed * INERTIA/ 2);
                    m_rectTransform.sizeDelta += new Vector2(0, -m_speed * INERTIA);
                    m_thornPosition += new Vector3(0, m_speed * INERTIA/2);



                    if (m_startPosition.y < m_rectTransform.position.y)
                    {
                        m_rectTransform.position = m_startPosition;
                        m_rectTransform.sizeDelta = m_startSize;
                        m_collider.size = m_startCollider;
                        m_reset = false;
                        yield return new WaitForSeconds(m_derayTime);
                    }

                }
                else
                {
                    m_collider.size += new Vector2(0, m_accele);
                    m_rectTransform.position += new Vector3(0, -m_accele / 2);
                    m_rectTransform.sizeDelta += new Vector2(0, m_accele);
                    m_thornPosition += new Vector3(0, -m_accele / 2);
                    m_accele = m_accele * INERTIA;
                
                    RaycastHit2D hitGround = Physics2D.BoxCast(m_startPosition + new Vector3(0f, -m_collider.size.y), new Vector2(1.5f, 2), 1f, Vector2.zero, 0, 1 << LayerMask.NameToLayer("Ground"));
                    if (hitGround)
                    {
                        m_reset = true;
                        yield return new WaitForSeconds(1f);
                        m_accele = m_speed;
                    }

                }
                if (!m_hited)
                {
                    RaycastHit2D hitPlayer = Physics2D.BoxCast(m_startPosition + new Vector3(0f, -m_collider.size.y), new Vector2(1.5f, 2), 1f, Vector2.zero, 0, 1 << LayerMask.NameToLayer("Player"));

					if (hitPlayer)
                    {
                        Hit(hitPlayer.collider.transform.root.GetComponent<PlayerComponent>());

                    }
                }
                yield return null;
            }

        }

        //void OnDrawGizmos()
        //{
        //    Gizmos.DrawWireCube(m_startPosition + new Vector3(0f, -m_collider.size.y), new Vector2(1.5f, 2));
        //}
        public void Hit(PlayerComponent arg_player)
        {
			if(arg_player == null) { return; }

			//m_hited = true;
            //プレイヤーに当たった時の処理はここにお願いします

			arg_player.Dead();
        }
        
    }
}
