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
    private void Update() //1�ʿ� 60������
    {
        if (Input.GetButtonUp ("Horizontal"))
        {
            rigid.velocity= new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //ĳ���� ���� ��ȯ
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            //trnas.localScale.x =Input.GetAxisRaw("Horizontal") == -1;  //�� �ȵǳ� ..
        }
           



    }

   
    void FixedUpdate()  // 1�ʿ� 50������
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x >= maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); //Right
        else if (rigid.velocity.x <maxSpeed*(-1))
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);//left
    }
}
