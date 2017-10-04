using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Oyama
{

    public class BombTest : MonoBehaviour
    {

        //爆弾本体
        GameObject m_mainObj;

        //アニメーター
        Animator m_anime;

        //爆破エフェクト
        GameObject m_brastEffect;

        //壁破壊エフェクト
        GameObject m_breakEffect;

        //リジッドボデェ
        Rigidbody2D m_rigid;

        //停止してから爆破に入る秒数
        float m_addTime = 0;
        float m_startExplosionTime = 0.3f;

        //爆破開始フラグ
        bool m_startExplosionFlag = false;
        

        // Use this for initialization
        void Start()
        {
            m_mainObj = transform.FindChild("Bomb").gameObject;

            m_anime = m_mainObj.GetComponent<Animator>();

            //Stopはアニメーターを停止してくれるが、その後再開できない糞メソッド
            //m_anime.Stop();

            m_brastEffect = Resources.Load<GameObject>("Effect/EF_Explosion");
            m_breakEffect = Resources.Load<GameObject>("Effect/EF_BreakWall");

            m_rigid = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

            //爆破開始したら処理しません
            if (m_startExplosionFlag) return;
            
            //動きが無くなった&爆破フラグが経っていないならタイム加算
            if(m_rigid.velocity.x == 0 && m_rigid.velocity.y == 0 && m_startExplosionFlag == false)
            {
                m_addTime += Time.deltaTime;
                Debug.Log("加算中");
            }
            else
            {
                m_addTime = 0;
                Debug.Log("加算リセット");
            }

            //指定秒動きを止めていたら爆破フラグオン
            if(m_startExplosionTime < m_addTime && m_startExplosionFlag == false)
            {
                Debug.Log("unko");
                m_anime.Play("Bomb");
                m_anime.Update(0);
                Debug.Log(m_anime.GetCurrentAnimatorStateInfo(0).length);

                //再生中のアニメ「Bomb」の再生時間を読み取って、その時間後に爆破エフェクト&接触判定を取って壁を壊したい
                Invoke("Explosion", m_anime.GetCurrentAnimatorStateInfo(0).length);


                m_startExplosionFlag = true;
                
            }

        }


        //爆破
        void Explosion()
        {
            //爆弾の表示を消す
            m_mainObj.GetComponent<SpriteRenderer>().enabled = false;

            //爆弾のコライダーの半径を取得し、爆風範囲とする
            float rad = GetComponent<CircleCollider2D>().radius;
            //爆風範囲はちょっと大きめで
            rad += rad * 2;
            
            List<RaycastHit2D> hit = new List<RaycastHit2D>();
            //hit = Physics2D.CircleCastAll(transform.position,rad, Vector3.forward, Mathf.Infinity);

            foreach(var col in Physics2D.CircleCastAll(transform.position, rad, Vector3.forward, Mathf.Infinity))
            {
                hit.Add(col);
            }

            for(int i = 0; i < hit.Count; i++)
            {
                if(hit[i].transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    //死亡判定
                    Debug.Log("死んだ");
                }
                else if(hit[i].transform.gameObject.layer == LayerMask.NameToLayer("BrokenWall"))
                {
                    Instantiate(m_breakEffect, hit[i].transform.position, hit[i].transform.rotation);
                    Destroy(hit[i].transform.gameObject);
                }
            }

            //リジッドボデェを切る
            m_rigid.Sleep();

            GameObject eff = Instantiate(m_brastEffect, transform.position, Quaternion.identity);
            eff.transform.parent = transform;

            Destroy(transform.gameObject, eff.GetComponent<ParticleSystem>().duration);

        }
        
    }

}