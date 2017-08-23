using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Title
{
    public class TitleAnimation : MonoBehaviour
    {
        Vector3 Move = new Vector3(0.05f, 0, 0);
        Vector3 Restart = new Vector3(-15f, -3.85f, 0);
        Animator animator;
        SpriteRenderer renderer;

        bool stop = true;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            renderer = GetComponent<SpriteRenderer>();
            StartCoroutine("Wait");
        }

        void Update()
        {

        }

        private IEnumerator Wait()
        {

            while (true)
            {

                if (transform.position.x >= 5.5f && stop)
                {
                    renderer.flipX = false;
                    animator.SetBool("RunFlag", false);
                    animator.SetBool("IdleFlag", true);
                    yield return new WaitForSeconds(4.0f);
                    stop = false;
                }
                else
                {
                    renderer.flipX = true;
                    animator.SetBool("RunFlag", true);
                    animator.SetBool("IdleFlag", false);
                    transform.position += Move;
                }

                if (transform.position.x >= 10f)
                {
                    transform.position = Restart;
                    stop = true;
                }


                yield return null;

            }
        }
    }

}
