using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class ElevatorHitCheck : MonoBehaviour
    {
        
        private Elevator m_elevator;
        
        [SerializeField]
        private bool m_isHit;
        private bool m_isRide;

        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            m_elevator = transform.parent.gameObject.GetComponent<Elevator>();
            m_isHit = false;
            m_isRide = false;
        }

        void Update()
        {

            //当たり判定
            m_isHit = Physics2D.BoxCast(transform.position + new Vector3(0f, 0.5f), new Vector2(0.2f, 0.2f), 0f, Vector2.one, 1f, 1 << LayerMask.NameToLayer("Player"));

            if (!IsAction()) return;

            //初めて乗ったとき
            if (m_isHit && !m_isRide)
            {
                m_isRide = true;
                StartCoroutine("StartDelay");
                
            }//さっきまで乗ってたとき
            else if(!m_isHit && m_isRide)
            {
                m_isRide = false;
            }

        }

#if UNITY_EDITOR 
        void OnDrawGizmos()
        {
            //Castの可視化
            Gizmos.DrawWireCube(transform.position + new Vector3(0f, 0.5f), new Vector2(0.2f, 0.2f));

        }
#endif
        //アニメーション中の待機時間
        IEnumerator StartDelay()
        {
            yield return new WaitForSeconds(0.2f);
            m_elevator.Action();
        }

        /// <summary>
        /// 動作しないとき,
        /// 動作中のとき
        /// </summary>
        bool IsAction()
        {
            return (m_isHit && m_isRide) || !(m_isHit && m_isRide);
        }
    }
}