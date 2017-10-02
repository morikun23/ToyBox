using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyor : MonoBehaviour {


    [SerializeField]
    private float m_movePower= 100.0f;

    

    void OnCollisionEnter2D(Collision2D other)
    {
           
        var body = other.gameObject.GetComponent<Rigidbody2D>();
        if (body != null)
        {
            Vector3 addPower = transform.position * m_movePower;
            body.AddForce(addPower, ForceMode2D.Force);

            //body.MovePosition(other.transform.position + add);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        var body = other.gameObject.GetComponent<Rigidbody2D>();
        if(body != null)
        {
            Vector3 addPower = transform.position * m_movePower;
            body.AddForce(addPower, ForceMode2D.Force);
        }
    }
    
}
