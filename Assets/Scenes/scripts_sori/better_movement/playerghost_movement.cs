using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerghost_movement : MonoBehaviour
{
    public Transform player_transform;
    public Rigidbody2D player_rigidbody;
    private new  Transform transform;
    public  Rigidbody2D ghost_rigidbody2D;
    [SerializeField]
    float speed = 5;
    [SerializeField]
    float jump_power = 10;


    // Start is called before the first frame update
    void Start()
    {
        ghost_rigidbody2D = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //player_transform.position = transform.position;
        transform.position = player_transform.position;
        float x = Input.GetAxisRaw("Horizontal");

        move(x);
    }

    void move(float x)
    {
        
        ghost_rigidbody2D.velocity = new Vector2(x * speed, player_rigidbody.velocity.y);

        
    }
}
