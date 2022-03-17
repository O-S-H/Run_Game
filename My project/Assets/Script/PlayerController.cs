using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Transform trnas;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer= GetComponent<SpriteRenderer>();
    }
    private void Update() //1초에 60프레임
    {
        if (Input.GetButtonUp ("Horizontal"))
        {
            rigid.velocity= new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //캐릭터 방향 전환
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            //trnas.localScale.x =Input.GetAxisRaw("Horizontal") == -1;  //왜 안되냐 ..
        }
           



    }

   
    void FixedUpdate()  // 1초에 50프레임
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x >= maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); //Right
        else if (rigid.velocity.x <maxSpeed*(-1))
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);//left
    }
}
