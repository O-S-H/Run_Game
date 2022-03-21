using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextmove;
    Animator anim;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Think();

        Invoke("Think", 5);
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextmove, rigid.velocity.y);
        Vector2 frontvec = new Vector2(rigid.position.x + nextmove*0.3f, rigid.position.y);
        Debug.DrawRay(frontvec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontvec, Vector3.down, 1, LayerMask.GetMask("Paltform"));

        if (rayHit.collider == null)
        {
            Turn();

        }



    }


    
    void Think()
    {
        nextmove = Random.Range(-1, 1);

       
        Invoke("Think", 4);
        anim.SetInteger("isWalk2", nextmove);

        if (nextmove != 0) 
        {
            spriteRenderer.flipX = nextmove == 1;
        }
        
    }
    void Turn()
    {
        nextmove *= -1;
        spriteRenderer.flipX = nextmove == 1;
        CancelInvoke();
        Invoke("Think", 5);
    }
}
