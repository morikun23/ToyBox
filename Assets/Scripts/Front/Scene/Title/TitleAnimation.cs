using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToyBox.Title
{
    public class TitleAnimation : MonoBehaviour
    {
        Vector3 Move = new Vector3(0.05f, 0, 0);
        Vector3 Restart = new Vector3(-15f, -3.35f, 0);
        Animator animator;
        SpriteRenderer renderer;

        [SerializeField]
        GameObject sp_Comment;

        TitleComment titleComment;


        bool stop = true;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            renderer = GetComponent<SpriteRenderer>();
            StartCoroutine("Wait");
            
        }

        private IEnumerator Wait()
        {
            titleComment = sp_Comment.GetComponent<TitleComment>();
            titleComment.Initialize();
            while (true)
            {

                if (transform.position.x >= 5.5f && stop)
                {
                    renderer.flipX = false;
                    animator.SetBool("RunFlag", false);
                    animator.SetBool("IdleFlag", true);
                    yield return new WaitForSeconds(3.0f);
                    titleComment.Spawn();
                    yield return new WaitForSeconds(1.0f);
                    titleComment.Delete();

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
