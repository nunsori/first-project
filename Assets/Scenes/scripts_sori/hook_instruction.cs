using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class hook_instruction : MonoBehaviour
{
    
    public LineRenderer line;
    
    public Transform hook;

    private Rigidbody2D rigidbody2d;

    hook_sprite hooks;

    Vector2 mousedir;

    Vector2 touchdir;

    Vector2 joysitck_dir;

    Vector2 line_go;

    public bool isHookActive;

    public bool isLineMax;

    public bool isAttach;

    private bool dashcount2 = true;

    public GameObject submenu_object;

    public GameObject defaultmenu_object;

    public Slider hook_duration;
    public GameObject hook_duration_object;


    private bool corutine_running = false;

    public AudioSource hook_source;

    public GameObject clear_menu_object;
    //public GameObject duration_slider_object;

    Touch touch;
    public bool type = true;

    public joy_stick_control joy_Stick_Control;

    public float max_distance = 6f;

    public bool wire_up = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        hooks = GetComponent<hook_sprite>();
        type = true;
        line.positionCount = 2;
        line.endWidth = line.startWidth = 0.05f;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);
        line.useWorldSpace = true;
        isAttach = false;
        hook.gameObject.SetActive(false);
        hook_duration_object.SetActive(false);
        //isHookActive = false;
        //duration_slider_object.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
        

        Debug.Log(isAttach);
        Debug.Log(isLineMax);
        Debug.Log(isHookActive);
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);
        /*
        if(Input.touchCount>0)
        {
            
            
                if (!IsPointerOverUIObject(touch.position))
                {
                    touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {


                        //touchdir = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
                        //hook_shoot();
                        //type = true;
                        
                    }
                }
            
        }*/

        /*
        if(Input.GetKeyDown(KeyCode.Mouse0) && !isHookActive && submenu_object.activeSelf == false && clear_menu_object.activeSelf == false&& EventSystem.current.IsPointerOverGameObject()==false && EventSystem.current.IsPointerOverGameObject(1) == false && EventSystem.current.IsPointerOverGameObject(0) == false)
        {
            //hook_shoot();
            type = false;
            hook_shoot();
        }
        */ //컴퓨터플레이 전용 스크립트!!!!


        /*
        if(Input.GetKeyDown(KeyCode.Mouse0)&& !isHookActive && submenu_object.activeSelf==false && clear_menu_object.activeSelf==false)
        {
            this.hook_source.Play();
            hook.position = transform.position;
            mousedir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            isHookActive = true;
            isLineMax = false;
            hook.gameObject.SetActive(true);
        }*/

        if(isHookActive&&!isLineMax&&!isAttach)
        {
            if (type==false)
            {
                Debug.Log("mouse");
                hook.Translate(mousedir.normalized * Time.deltaTime * 40);
            }
            else
            {
                Debug.Log("holymoly");
                hook.Translate(joysitck_dir.normalized * Time.deltaTime * 40);
            }


            if(Vector2.Distance(transform.position,hook.position)>max_distance)
            {
                isLineMax = true;
            }
        }
        else if (isHookActive && isLineMax && !isAttach)
        {
           // hook.position = Vector2.MoveTowards(transform.position, hook.position, Time.deltaTime * 15);
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime*40);
            if (Vector2.Distance(transform.position,hook.position)<0.1f)
            {
                isHookActive = false;
                isLineMax = false;
                hook.gameObject.SetActive(false);
            }
        }
        else if(isAttach)
        {
            //duration_slider_object.SetActive(true);
            //duration_slider_object.transform.position = gameObject.transform.position + new Vector3(0, 3, 0);

            if (corutine_running == false)
            {
                StartCoroutine("hook_point");
            }
            else if (corutine_running == true)
            {

            }

            if (GetComponent<player_move>().dashcount == false && dashcount2 == true)
            {
                GetComponent<player_move>().dashcount = true;
            }
            hook.GetComponent<hook_sprite>().joint2d.distance = Vector2.Distance(transform.position, hook.position);
            
            // line_go = hook.position - transform.position;
            //rigidbody2d.AddForce(line_go.normalized*Time.deltaTime*150);

            //Debug.Log(rigidbody2d.velocity);
            //hooks.joint2d.distance = 0.1f;
            
            //line_go = rigidbody2d.velocity;

            /*
            if (Input.GetKeyDown(KeyCode.Mouse0)&& EventSystem.current.IsPointerOverGameObject() == false || gameObject.layer == 8)
            {
                StopCoroutine("hook_point");
                hook_duration_object.SetActive(false);
                corutine_running = false;
                hook_duration.value = 3;

                //hook.GetComponent<hook_sprite>().joint2d.distance = 3;

                isAttach = false;
                isHookActive = false;
                isLineMax = false;
                hook.GetComponent<hook_sprite>().joint2d.enabled = false;
                hook.gameObject.SetActive(false);
                //rigidbody2d.velocity = (line_go);
                dashcount2 = true;
            }*/ //컴퓨터플레이 전용스크립트!!

            if(gameObject.layer == 8)
            {
                StopCoroutine("hook_point");
                hook_duration_object.SetActive(false);
                corutine_running = false;
                hook_duration.value = 3;

                //hook.GetComponent<hook_sprite>().joint2d.distance = 3;

                isAttach = false;
                isHookActive = false;
                isLineMax = false;
                hook.GetComponent<hook_sprite>().joint2d.enabled = false;
                hook.gameObject.SetActive(false);
                //rigidbody2d.velocity = (line_go);
                dashcount2 = true;
            } //모바일 전용 스크립트

            if (Input.GetKey(KeyCode.Mouse1) || wire_up == true)
            {
                hook.GetComponent<hook_sprite>().joint2d.distance -= 0.1f;
            }

            
            /*
            if(Input.touchCount>0)
            {
                
                
                    if (!IsPointerOverUIObject(touch.position))//EventSystem.current.IsPointerOverGameObject(0) == false)
                    {
                        touch = Input.GetTouch(0);
                        if (touch.phase == TouchPhase.Began)
                        {


                            StopCoroutine("hook_point");
                            hook_duration_object.SetActive(false);
                            corutine_running = false;
                            hook_duration.value = 3;

                            //hook.GetComponent<hook_sprite>().joint2d.distance = 3;

                            isAttach = false;
                            isHookActive = false;
                            isLineMax = false;
                            hook.GetComponent<hook_sprite>().joint2d.enabled = false;
                            hook.gameObject.SetActive(false);
                            //rigidbody2d.velocity = (line_go);
                            dashcount2 = true;

                            

                        }
                    }
                
            }*/

        }

        /*
        if( Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKeyDown(KeyCode.Mouse1))
        {
            rigidbody2d.AddForce(new Vector2(0, 400));
            Debug.Log("updash");
        }*/

        /*if (Input.GetKeyDown(KeyCode.R))
        {
            
            rigidbody2d.AddForce(new Vector2(400,0));
        }*/

        if (Input.GetKeyDown(KeyCode.R))
        {
            dashcount2 = false;
        }
    }

    IEnumerator hook_point()
    {
        corutine_running = true;
        float durationtime = 3;
        //yield return new WaitForSeconds(4);
        hook_duration_object.SetActive(true);

        while (durationtime >= 0)
        {
            
            yield return new WaitForSeconds(0.1f);
            durationtime -= 0.1f;
            hook_duration.value -= 0.1f;
        }

        hook_duration_object.SetActive(false);

        isAttach = false;
        isHookActive = false;
        isLineMax = false;
        hook.GetComponent<hook_sprite>().joint2d.enabled = false;
        hook.gameObject.SetActive(false);
        //rigidbody2d.velocity = (line_go);
        dashcount2 = true;
        StopCoroutine("hook_point");
        corutine_running = false;
        hook_duration.value = 3;

    }

    public void hook_shoot()
    {
        this.hook_source.Play();
        hook.position = transform.position;
        mousedir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        touchdir = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
        if (type != false)
        {
            joysitck_dir = joy_Stick_Control.Hook_dir_transform - transform.position;
        }
        isHookActive = true;
        isLineMax = false;
        hook.gameObject.SetActive(true);
    }

    public void dash_inst()
    {
        if(isAttach)
        {
            dashcount2 = false;
        }
    }

    public void fasten_wire()
    {
        if(isAttach)
        {
            hook.GetComponent<hook_sprite>().joint2d.distance -= 0.1f;
        }
    }

    public bool IsPointerOverUIObject(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition
            = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = touchPos;

        List<RaycastResult> results = new List<RaycastResult>();


        EventSystem.current
        .RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }

    public void eject_wire()
    {
        StopCoroutine("hook_point");
        hook_duration_object.SetActive(false);
        corutine_running = false;
        hook_duration.value = 3;

        //hook.GetComponent<hook_sprite>().joint2d.distance = 3;

        isAttach = false;
        isHookActive = false;
        isLineMax = false;
        hook.GetComponent<hook_sprite>().joint2d.enabled = false;
        hook.gameObject.SetActive(false);
        //rigidbody2d.velocity = (line_go);
        dashcount2 = true;

    }
}
