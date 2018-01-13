//担当：佐藤　由樹
//概要：各イベントクラスのインターフェイス
//参考：IPlayerState

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public interface IEvent 
    {
        void OnStart(Player arg_player,GameObject arg_gimmick);
        bool OnUpdate();
        void OnEnd();


    }
}