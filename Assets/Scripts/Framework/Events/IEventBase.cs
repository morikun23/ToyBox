//担当：佐藤　由樹
//概要：各イベントクラスのインターフェイス
//参考：IPlayerState

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public interface IEventBase 
    {
        void OnStart();
        bool OnUpdate();
        void OnEnd();


    }
}