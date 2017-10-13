using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public Dialog() { }


    void UpdateByFrame()
    {
        if (transform.localScale.x <= 2.5f)
        {
            transform.localScale += new Vector3(0.05f, 0, 0);
        }
    }

    void Update()
    {
        UpdateByFrame();
    }
}


