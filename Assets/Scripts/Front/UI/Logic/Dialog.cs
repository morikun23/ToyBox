using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    

    public Dialog() { }

    public Dialog(Transform arg_dialog) { m_dialogBuf = arg_dialog; }

    //自身のTransform
    private Transform m_dialogBuf;


    public Transform m_dialog
    {
        get
        {       if (m_dialogBuf == null)
            {
                m_dialogBuf = this.transform;
            }
            return m_dialogBuf;
        }
    }

    public void SetDialog(Transform arg_dialog)
    {
        m_dialogBuf = arg_dialog;
    }


    void Initialize()
    {
        
    }
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


