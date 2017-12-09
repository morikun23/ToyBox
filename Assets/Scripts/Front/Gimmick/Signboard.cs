using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class Signboard : FixedItem
    {

        //生成するオブジェクト
        public GameObject m_diaPrefab;

        //生成したオブジェクト格納用
        GameObject m_Prefab;

        //オブジェクト生成場所
        public Vector3 m_formPos;

        //伸縮フラグ(参照渡し用)
        public bool m_ExtendFlg = true;

        [SerializeField]
        Sprite m_picture;

        public override void OnGraspedEnter(PlayerComponent arg_player)
        {
            //ダイアログ生成
            m_Prefab = Instantiate(m_diaPrefab, m_formPos, Quaternion.identity);
            m_Prefab.transform.Find("Picture").GetComponent<SpriteRenderer>().sprite = m_picture;
            m_Prefab.GetComponent<Dialog>().Init(this);
            

            //音じゃ
            //AudioSource source = AppManager.Instance.m_audioManager.CreateSe("SE_Dialog_open");
            //source.Play();

            m_flg_ableGrasp = false;
            m_flg_ableReleace = false;
        }

        public override void OnGraspedStay(PlayerComponent arg_player)
        {
            //ダイアログ伸縮
            m_ExtendFlg = true;
            m_flg_ableReleace = true;
        }

        
        public override void OnGraspedExit(PlayerComponent arg_player)
        {
            m_ExtendFlg = false;

            //音じゃ
            //AudioSource source = AppManager.Instance.m_audioManager.CreateSe("SE_Dialog_close");
            //source.Play();

            arg_player.Arm.m_shorten = true;
            m_flg_ableGrasp = true;
            m_flg_ableReleace = false;
        }

    }
}
