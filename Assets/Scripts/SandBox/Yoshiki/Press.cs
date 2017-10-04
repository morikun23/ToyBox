using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Yoshiki
{
    public class Press : MonoBehaviour
    {
        Rigidbody2D rigidbody2d;

        [SerializeField]
        float m_speed;

        [SerializeField]
        float m_startTime;

        bool reset = false;

        void Start()
        {
            Initialize();
        }
        void Update()
        {
            StartCoroutine("UpdateByFrame");
        }

        public void Initialize()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        public IEnumerator UpdateByFrame()
        {
            yield return new WaitForSeconds(m_startTime);

            if (reset)
            {
                rigidbody2d.AddForce(Vector3.up * m_speed);
            }
            else
            {
                rigidbody2d.AddForce(Vector3.down * m_speed);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {

            if(other.name == "Saucer")
            {
                reset = true;
                rigidbody2d.velocity = Vector3.zero;
            }
            if (other.name == "ResetPos")
            {
                reset = false;
                rigidbody2d.velocity = Vector3.zero;
            }
        }

    }
}
