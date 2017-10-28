using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class Bomb : MonoBehaviour
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

        float m_ToExplosionTime = 0;
        float m_EffectTotalTime = 0;

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
            

            //動きが無くなった&爆破フラグが経っていないならタイム加算
            if (Mathf.Abs(m_rigid.velocity.x) <= 1 && Mathf.Abs(m_rigid.velocity.y) <= 1 && m_startExplosionFlag == false)
            {
                m_addTime += Time.deltaTime;
            }
            else
            {
                m_addTime = 0;
            }

            //指定秒動きを止めていたら爆破フラグオン
            if (m_startExplosionTime < m_addTime && m_startExplosionFlag == false)
            {
                m_anime.Play("Bomb");
                m_anime.Update(0);

                //再生中のアニメ「Bomb」の再生時間を読み取って、その時間後に爆破エフェクト&接触判定を取って壁を壊したい
                //Invoke("Explosion", m_anime.GetCurrentAnimatorStateInfo(0).length);

                m_EffectTotalTime = m_anime.GetCurrentAnimatorStateInfo(0).length;

                m_startExplosionFlag = true;

            }

            if (m_startExplosionFlag)
            {

                m_ToExplosionTime += Time.deltaTime;

                if (m_EffectTotalTime < m_ToExplosionTime)
                {
                    Explosion();
                    m_EffectTotalTime = 200;
                }
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

            //音じゃ
            AudioSource source = AppManager.Instance.m_audioManager.CreateSe("SE_Bomb_expload");
            source.Play();

            List<RaycastHit2D> hit = new List<RaycastHit2D>();

            foreach (var col in Physics2D.CircleCastAll(transform.position, rad, Vector3.forward, Mathf.Infinity))
            {
                hit.Add(col);
            }

            for (int i = 0; i < hit.Count; i++)
            {
				if (hit [i].transform.gameObject.layer == LayerMask.NameToLayer ("Player")) {
					if (hit [i].collider.gameObject.name != "ArmCollider") {
						//死亡判定
						hit [i].transform.gameObject.GetComponent<PlayerComponent> ().Dead ();
					}
				}
                else if (hit[i].transform.gameObject.tag == "BrokenWall")
                {
                    GameObject eff_breakwall = Instantiate(m_breakEffect, hit[i].transform.position, hit[i].transform.rotation);
                    Destroy(hit[i].transform.gameObject);

                    Destroy(eff_breakwall, eff_breakwall.GetComponent<ParticleSystem>().duration);
                }
            }

            //リジッドボデェを切る
            m_rigid.Sleep();

            GameObject eff_brast = Instantiate(m_brastEffect, transform.position, Quaternion.identity);
            eff_brast.transform.parent = transform;

            Destroy(transform.gameObject, eff_brast.GetComponent<ParticleSystem>().duration);

        }

    }

}