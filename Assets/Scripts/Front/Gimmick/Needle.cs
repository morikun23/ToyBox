using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class Needle : MonoBehaviour
    {

        void OnTriggerEnter2D(Collider2D arg_col)
        {
            if (arg_col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log("Dead");
                //プレイヤーを殺す何か
            }
        }

    }
}