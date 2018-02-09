using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Kaihatsu {

    public class testInitialize : MonoBehaviour
    {

        [SerializeField]
        UIButton unko;

        [SerializeField]
        GameObject modal;

        public System.Action m_hoge;

        // Use this for initialization
        void Start()
        {
            unko.Initialize(new ButtonAction(ButtonEventTrigger.OnRelease, () => { UIManager.Instance.PopupOptionModal(() => { }); }));
        }
    }
}


         