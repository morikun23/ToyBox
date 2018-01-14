using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{

    public class Signboard : ImmobilizedItem
    {

        //生成するオブジェクト
        public GameObject m_diaPrefab;

        //生成したオブジェクト格納用
        private GameObject m_Prefab;

        //オブジェクト生成場所
        public Vector3 m_formPos;

        //伸縮フラグ(参照渡し用)
        public bool m_ExtendFlg = true;

        [SerializeField]
        Sprite m_picture;


        public override void OnGraspedEnter(Player arg_player)
        {
            //ダイアログ生成
            m_Prefab = Instantiate(m_diaPrefab, m_formPos, Quaternion.identity);
            m_Prefab.transform.Find("Picture").GetComponent<SpriteRenderer>().sprite = m_picture;
            m_Prefab.GetComponent<TutorialDialog>().Init(this);

            //新しい音でして
            AudioManager.Instance.QuickPlaySE("SE_Dialog_open");

			SetAbleGrasp(false);
			SetAbleRelease(false);
        }

        public override void OnGraspedStay(Player arg_player)
        {
            //ダイアログ伸縮
            m_ExtendFlg = true;
			SetAbleRelease(true);
        }

        
        public override void OnGraspedExit(Player arg_player)
        {
            m_ExtendFlg = false;

            //新しい音でして
             AudioManager.Instance.QuickPlaySE("SE_Dialog_close");


			SetAbleGrasp(true);
			SetAbleRelease(false);
		}
    }
}
