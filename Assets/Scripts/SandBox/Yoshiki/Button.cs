 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Yoshiki
{
    public class Button : ToyBox.MobileInput
    {
        protected Playable m_playable;

        protected Animator m_animator;

        protected Vector2 worldPoint;

        void OnDown() {
            Started();
        }

        void OnPress() { }

        void OnUp() {
            TouchEnd();
        }


    }
}