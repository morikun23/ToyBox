using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Title
{
    public class TitleComment : MonoBehaviour {

        SpriteRenderer renderer;

        public void Initialize()
        {
            renderer = this.GetComponent<SpriteRenderer>();
        }

        public void Spawn()
        {

            //Color color = renderer.color;
            //color.a = 1;
            //renderer.color = color;


        }
        public void Delete()
        {
            //Color color = renderer.color;
            //color.a = 0;
            //renderer.color = color;

        }
    }
}