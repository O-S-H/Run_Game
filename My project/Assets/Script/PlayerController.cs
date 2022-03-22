using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Gamemanager gamemanager;
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    private bool stepped = false;
    public GameObject gameoverUI;





    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
       
    }
  
    void Update() //1초에 60프레임
    {
        
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //캐릭터 방향 전환
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;


        }
        if (Mathf.Abs(rigid.velocity.x) < 0.5f)
        {
            anim.SetBool("isWalk", false);

        }
        else
        {
            anim.SetBool("isWalk", true);
        }

        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);


        }
        




    }


    void FixedUpdate()  // 1초에 50프레임
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x >= maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); //Right
        }
          
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);//left
        }

        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Paltform"));
          
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 3)
                {
                    anim.SetBool("isJump", false);
                }

            }
        }
        
    }
     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enermy")
        {
            if (rigid.position.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else
            {
                onDamaged(collision.transform.position);
            }
            if (collision.gameObject.tag == "Player" )
            {
                stepped = true;
                Gamemanager.instance.AddScore(1);
            }

        }

        
        void  OnAttack(Transform  other)

        {

            rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            Enermy enermy = other.GetComponent<Enermy>();
            enermy.onDamaged();


        }

        
    }
    
    void onDamaged(Vector2 targetpos)
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        int dirc = transform.position.x - targetpos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*7, ForceMode2D.Impulse);
        
        //Animation
        anim.SetTrigger("isDamged");
        
        
        //
        Invoke("offDamaged", 1f);

        
    }

    void offDamaged()
    {
        gameObject.layer = 7;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    private void Die()
    {
        rigid.velocity = Vector2.zero;
       
        Gamemanager.instance.OnplayerDead();
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (collision.gameObject.tag == "coin")
        {

            
            collision.gameObject.SetActive(false);
            
           
        }
        if (collision.gameObject.tag == "Dead")
        {

            gameoverUI.SetActive(true);

        }

        



    }
    


} 
