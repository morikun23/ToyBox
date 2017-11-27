using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Yoshiki
{
    public class ElevatorManager : MonoBehaviour
    {
        enum m_Direction
        {
            m_right,
            m_left
        }

        Elevator m_elevator;

        Vector3 m_correction = new Vector3(0.3f, 0);

        //開いてる方
        [SerializeField]
        m_Direction m_direction;

        bool m_isHit;
        bool m_isRide;

        void Start()
        {
            Initialize();
        }

        // Use this for initialization
        void Initialize()
        {
            m_elevator = transform.parent.gameObject.GetComponent<Elevator>();
            StartCoroutine("UpdateByFrame");
            m_isRide = false;
        }


        // Update is called once per frame
        IEnumerator UpdateByFrame()
        {
            while (true)
            {
                if (m_direction == m_Direction.m_right)
                {
                    // ＿＿
                    //|
                    //|＿＿  の場合
                    m_isHit = Physics2D.Raycast(transform.position - m_correction, Vector3.right, 0.3f, 1 << LayerMask.NameToLayer("Player"));
                }
                else
                {
                    //＿＿
                    //　　|
                    //＿＿|  の場合
                    m_isHit = Physics2D.Raycast(transform.position + m_correction, Vector3.left, 0.3f, 1 << LayerMask.NameToLayer("Player"));
                }

                Debug.Log(m_isHit);

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

        void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position - m_correction, Vector3.right);
        }


    }
}