using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class ElevatorHitCheck : MonoBehaviour
    {
        private Elevator m_elevator;

        private Vector3 m_correction = new Vector3(0.3f, 0);
        
        private bool m_isHit;
        private bool m_isRide;

        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            m_elevator = transform.parent.gameObject.GetComponent<Elevator>();
            StartCoroutine("UpdateByFrame");
            m_isHit = false;
            m_isRide = false;
        }

        IEnumerator UpdateByFrame()
        {
            while (true)
            {
                m_isHit = Physics2D.BoxCast(transform.position + new Vector3(0f, 0.5f), new Vector2(0.2f, 0.2f), 0f, Vector2.one, 1f);

                if (m_isHit && !m_isRide)
                {
                    m_isRide = true;
                    yield return new WaitForSeconds(2f);
                    m_elevator.Action();
                    
                }
                else if(!m_isHit && m_isRide)
                {
                    m_isRide = false;
                }
                yield return null;
            }

        }

#if UNITY_EDITOR 
        void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position + new Vector3(0f, 0.5f), Vector2.one);

        }
#endif

    }
}