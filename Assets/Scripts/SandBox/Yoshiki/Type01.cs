using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Yoshiki{
    public class Type01 : MonoBehaviour
    {

        Scene TitleScene;

        // Use this for initialization
        void Start()
        {
            TitleScene = new Title.TitleScene();
            TitleScene.OnEnter();
        }

        // Update is called once per frame
        void Update()
        {
            TitleScene.OnUpdate();
        }
    }
}
