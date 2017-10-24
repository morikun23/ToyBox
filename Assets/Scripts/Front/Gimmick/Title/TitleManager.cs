using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {

    private static TitleManager m_instance;

    public static TitleManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new GameObject("TitleManager").AddComponent<TitleManager>();
            }
            return m_instance;
        }
    }

    //「タッチでスタート！！」
    public SpriteRenderer m_startMassage;

    //フェード用
    public SpriteRenderer m_filter;
    float alphaAdd = 0.015f;

    bool next;

    public AudioClip sound;

    // Use this for initialization
    void Start () {
        m_filter.color = new Color(1, 1, 1, 0);
        next = false;
    }
	
	// Update is called once per frame
	void Update () {

        //タッチでスタート！！の点滅
        m_startMassage.color = new Color(1, 1, 1, Mathf.PingPong(Time.time,1));

        if(next) m_filter.color += new Color(0, 0, 0, alphaAdd);

        if(m_filter.color.a > 1)
        {
            Application.LoadLevel(1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioSource.PlayClipAtPoint(sound, Camera.main.gameObject.transform.position);
            next = true;
        }
        if (Input.touchCount < 1) return;
        if (Input.GetTouch(0).phase == TouchPhase.Began && next == false)
        {
            //シーン移動したい
            AudioSource.PlayClipAtPoint(sound, Camera.main.gameObject.transform.position);
            next = true;
            Debug.Log("in");
        }
    }
    

}
