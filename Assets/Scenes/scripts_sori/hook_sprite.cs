using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook_sprite : MonoBehaviour
{
    hook_instruction grappling;

    

    public DistanceJoint2D joint2d;

    public bool pull_line=false;

    // Start is called before the first frame update
    void Start()
    {
        //find �Լ� ���ؼ� player�� �ִ� hook_insturction c#������Ʈ �����ͼ� ������ ���ٰ�����
        grappling = GameObject.Find("player").GetComponent<hook_instruction>();
        joint2d = GetComponent<DistanceJoint2D>();
        
        
        //fff
    }


    private void FixedUpdate()
    {
        if(pull_line)
        {
            //joint2d.distance -= 0.01f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("enter1");
        if(collision.CompareTag("Ring"))
        {
            joint2d.enabled = true;
            grappling.isAttach = true;
            
            Debug.Log("enter");
            //joint2d.distance = 0.1f;
            pull_line = true;
        }
        else if (collision.CompareTag("notground"))
        {
            joint2d.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
