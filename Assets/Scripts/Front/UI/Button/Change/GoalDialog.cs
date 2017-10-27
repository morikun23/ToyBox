using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox
{
    public class GoalDialog : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine("UpdateByFrame");
        }

        IEnumerator UpdateByFrame()
        {
            yield return new WaitForSeconds(2f);

            while (true)
            {
                Extend();
                yield return null;
            }
        }

        //ダイアログを一定値まで伸縮
        void Extend()
        {
            if (transform.localScale.x < 2.5f)
            {
                transform.localScale += new Vector3(0.05f, 0, 0);
            }
        }

    }
}