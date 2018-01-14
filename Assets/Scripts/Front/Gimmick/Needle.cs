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
                //プレイヤーを殺す何か
                arg_col.transform.root.GetComponent<Player>().Dead();
            }
        }

    }
}