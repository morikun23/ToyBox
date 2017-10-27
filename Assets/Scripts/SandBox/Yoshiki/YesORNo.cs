using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox {
    public class YesORNo : Button{

        public enum YorN
        {
            YES = 1,
            NO = -1
        }

        [SerializeField]
        private YorN m_type;

        public override void OnDown()
        {
            base.OnDown();
        }

        public override void OnPress()
        {
            base.OnPress();
        }

        public override void OnUp()
        {
            base.OnUp();

            //GetComponent<Chosies>().answer = m_type;

        }

    }
}