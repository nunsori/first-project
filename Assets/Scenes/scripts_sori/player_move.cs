using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_move : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private new Transform transform;
    [SerializeField]
    float speed = 5;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField]
    float jump_power = 10;


    [SerializeField]
    private LayerMask groundlayer;
    private CircleCollider2D circlecollider2d;
    private bool isgrounded;
    private Vector3 footpsosition;


    public bool dashcount = true;
    
    

    hook_instruction grapling;

    public GameObject gameplaymanager;
    public Game_Play_Manager gameplaymanager_script;

    //public AudioListener audio_listner;

    public AudioSource jump_audio;
    public AudioSource hook_audio;
    public AudioSource dash_audio;
    public AudioSource hit_audio;

    public AudioClip jump_audio_2;
    public AudioClip hook_audio_2;

    public Slider audio_volume;

    public Button left_button;


    //mobile button value

    int left = 0;
    int right = 0;

    bool left_active;
    bool right_active;

    bool corutin_doubleclick_on = false;

    void Awake()
    {
        transform = GetComponent<Transform>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        circlecollider2d = GetComponent<CircleCollider2D>();
        grapling = GetComponent<hook_instruction>();
        //gameObject.transform.position = new Vector3(-3, 10, 0);
        gameplaymanager_script = gameplaymanager.GetComponent<Game_Play_Manager>();
        //audio_listner = GetComponent<AudioListener>();
        //this.jump_audio.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isgrounded);
        Bounds bounds = circlecollider2d.bounds;

        footpsosition = new Vector2(bounds.center.x, bounds.min.y-0.05f);

        //isgrounded = Physics2D.OverlapCircle(footpsosition, 0.1f, groundlayer);
        isgrounded = Physics2D.OverlapBox(footpsosition,new Vector2(0.8f,0.3f),0 ,groundlayer);

        jump_audio.volume = audio_volume.value;
        hook_audio.volume = audio_volume.value;
        dash_audio.volume = audio_volume.value;
        hit_audio.volume = audio_volume.value;
        

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //mobile
        if (left_active || right_active)
        {
            x = 0 - left + right;
        }

        rigidbody2D.rotation = 0;
        

        if(x<0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("isrun", true);
        }
        else if (x>0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("isrun", true);
        }
        else if (x==0&& Input.GetKeyDown(KeyCode.A)!=true&&Input.GetKeyDown(KeyCode.D)!=true)
        {
            animator.SetBool("isrun", false);
        }

        move(x);

        if(Input.GetKeyDown(KeyCode.Space)&& isgrounded == true)
        {
            animator.SetTrigger("click_jump");
            jump();
        }

        if(isgrounded == true)
        {
            animator.SetBool("isground", true);
            //dashcount = true;
        }
        else
        {
            animator.SetBool("isground", false);
        }


        if(Input.GetKeyDown(KeyCode.R))
        {
            dash();
        }
        /*
        if (Input.GetKeyDown(KeyCode.R) && dashcount == true && grapling.isAttach == true)//dash
        {
            if (spriteRenderer.flipX)
            {
                dash_audio.Play();
                //rigidbody2D.AddForce(new Vector2(-1200, 0));
                dash(true);
            }
            else if (!spriteRenderer.flipX)
            {
                dash_audio.Play();
                //rigidbody2D.AddForce(new Vector2(1200, 0));
                dash(false);
            }

            dashcount = false;
        }
        */

        Debug.Log(dashcount);

        
        if(Input.GetKeyDown(KeyCode.P))
        {
            transform.position = new Vector3(88, 114, 0);
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            transform.position = new Vector3(0, 0, 0);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(footpsosition, 0.1f);
        Gizmos.DrawCube(footpsosition, new Vector2(0.8f, 0.3f));
    }

    void move(float x)
    {
        if (gameObject.layer != 8)
        {
            if (grapling.isAttach)
            {
                rigidbody2D.AddForce(new Vector2(x * speed * 3, 0));
            }
            else if (Mathf.Abs(rigidbody2D.velocity.x) <= Mathf.Abs(x * speed))
            {
                rigidbody2D.velocity = new Vector2(x * speed, rigidbody2D.velocity.y);
            }
            else if (x == 0 && isgrounded) //바닥에 마찰없이 달라붙게되는 원인
            {
                rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            }
        }
        
        
        /*
        else if (isgrounded)
        {
            if(x<0)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else if (x>0)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            
            
            rigidbody2D.velocity = new Vector2(x * speed, rigidbody2D.velocity.y);

        }
       else if (!isgrounded)
        {
            if (x < 0)
            {
                transform.position += Vector3.left * (speed/2) * Time.deltaTime;
            }
            else if (x > 0)
            {
                transform.position += Vector3.right * (speed/2) * Time.deltaTime;
            }

        }*/     
    }

    public void jump()
    {
        
        if (gameObject.layer != 8 && isgrounded == true)
        {
            rigidbody2D.velocity = Vector2.up * jump_power;
            this.jump_audio.Play();
        }
    }

    //weaponCollider.SetActive(true)     true 대신 !weaponCollider.activeInHierarch 로 넣으면 무기 장착여부에따라 비활성화,활성화 가능

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.layer == 7)
        {
            Debug.Log("player_enter_enemy");
            hit_audio.Play();
            ondamaged(collision.transform.position);
        }

        if(collision.gameObject.layer ==10)
        {
            Debug.Log("clear game");
            gameplaymanager_script.game_clear = true;
        }
    }

    private void ondamaged(Vector2 targetpos)
    {
        //change layer
        gameObject.layer = 8;
        //view alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        //reation force
        int dir = transform.position.x - targetpos.x > 0 ? -1 : 1;
        rigidbody2D.AddForce(new Vector2(dir, 1) * 6, ForceMode2D.Impulse);

        //animation
        animator.SetTrigger("damage_trigger");

        Invoke("offdamaged", 3);
    }

    private void offdamaged()
    {
        gameObject.layer = 9;

        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    //mobile movement

    public void button_down(string type)
    {
        switch (type)
        {
            case "L":
                left = 1;
                left_active = true;
                if(!corutin_doubleclick_on)
                {
                    StartCoroutine("double_click_time");
                    
                }
                else if (corutin_doubleclick_on)
                {
                    Debug.Log("dashon");
                    dash();
                    grapling.dash_inst();
                    StopCoroutine("double_click_time");
                    corutin_doubleclick_on = false;
                    
                }
                break;
            case "R":
                right = 1;
                right_active = true;
                if (!corutin_doubleclick_on)
                {
                    StartCoroutine("double_click_time");

                }
                else if (corutin_doubleclick_on)
                {
                    Debug.Log("dashon");
                    dash();
                    grapling.dash_inst();
                    StopCoroutine("double_click_time");
                    corutin_doubleclick_on = false;

                }
                break;
            case "JUMP":
                jump();
                if(grapling.isAttach)
                {
                    grapling.wire_up = true;
                }
                break;
            case "DASH":
                break;
            case "UP_W":
                break;
        
        }
        
    }

    public void button_up(string type)
    {
        switch (type)
        {
            case "L":
                left = 0;
                left_active = false;
                break;
            case "R":
                right = 0;
                right_active = false;
                break;
            case "JUMP":
                if (grapling.isAttach)
                {
                    grapling.wire_up = false;
                }
                break;
            case "DASH":
                break;
            case "UP_W":
                break;

        }
    }

    public void dash()
    {
        if (dashcount == true && grapling.isAttach == true)//dash
        {
            if (spriteRenderer.flipX)
            {
                dash_audio.Play();
                rigidbody2D.AddForce(new Vector2(-1200, 0));
                //dash(true);
            }
            else if (!spriteRenderer.flipX)
            {
                dash_audio.Play();
                rigidbody2D.AddForce(new Vector2(1200, 0));
               // dash(false);
            }

            dashcount = false;
        }
    }
    IEnumerator double_click_time()
    {
        Debug.Log("double click corutine on");
        corutin_doubleclick_on = true;
        Debug.Log("double click corutine on"+corutin_doubleclick_on);
        yield return new WaitForSeconds(0.7f);
        Debug.Log("end_doubleclick corutine");
        StopCoroutine("double_click_time");
        corutin_doubleclick_on = false;

    }

}
