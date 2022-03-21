using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextmove;
   
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Think();
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextmove, rigid.velocity.y);
    }


    
    void Think()
    {
        nextmove = Random.Range(-1, 0);  
    }
}
