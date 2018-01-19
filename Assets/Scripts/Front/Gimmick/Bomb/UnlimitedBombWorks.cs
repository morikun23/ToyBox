using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class UnlimitedBombWorks : MonoBehaviour
    {

        //爆弾の入れ物
        GameObject m_bomb;

        //爆弾プレハブの入れ物
        GameObject m_PFbomb;

        //爆弾プレハブへのパス
        string m_bombPass = "Contents/Gimmick/Bomb/Prefabs/GM_Bomb";

        //何秒間隔で再度落とすか
        [SerializeField]
        float m_nextWorkTime = 1;

        float m_addTime = 0;

        //どこから落とすか
        [SerializeField]
        GameObject m_generatePoint;

        //カメラに写ってるかどうか判定
        bool m_triggerFlag;

        // Use this for initialization
        void Start()
        {
            m_PFbomb = Resources.Load<GameObject>(m_bombPass);
            m_triggerFlag = false;


        }

        // Update is called once per frame
        void Update()
        {

            m_addTime += Time.deltaTime;

            if(m_bomb == null && m_addTime > m_nextWorkTime && m_triggerFlag)
            {
                m_bomb = Instantiate(m_PFbomb, m_generatePoint.transform.position, Quaternion.identity);
                
            }

        }

        void OnBecameVisible()
        {
            m_triggerFlag = true;
        }

        void OnBecameInvisible()
        {
            m_triggerFlag = false;
        }

    }

}