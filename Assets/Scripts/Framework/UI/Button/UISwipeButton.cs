using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using ToyBox;

namespace ToyBox {
    public class UISwipeButton :  UIButton{

        private Vector3 neoPos;
        private Vector3 middlePos;
        GameObject neoFrame;
        bool IsMoving = true;
        protected sealed override void Swipe(PointerEventData data)
        {
            //ボタンの枠オブジェクトを取得(あまりよくないとは思いつつ)
            neoFrame = GameObject.Find("UiButtonFrame");

            //Debug.Log("SWIP");
            //base.Swipe(data);
            //OnReleased();
            //foreach (var action in m_btnActions.FindAll(_ => _.m_trigger == ButtonEventTrigger.OnSwaipe)){
            //    ExecCallBack(action);

            // }
            neoPos = transform.position;
            middlePos = neoPos - Camera.main.ScreenToWorldPoint(data.position);

            /// <summary>
            /// 範囲内にいるときはボタンが動ける
            /// </summary>
            if (IsMoving)
                transform.position -= middlePos;

            /// <summary>
            //ボタンの動ける範囲内は取得した
            //枠オブジェクトの中心から +-1(※ゴリ押し)
            /// </summary>

            if (transform.position.x >= neoFrame.transform.position.x + 1 || transform.position.x <= neoFrame.transform.position.x - 1 ||
                transform.position.y >= neoFrame.transform.position.y + 1 || transform.position.y <= neoFrame.transform.position.y - 1){

                /// <summary>
                /// 動きを止める
                /// </summary>
                IsMoving = false;
            }

        }
    }
}