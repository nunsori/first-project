using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_player_movement : MonoBehaviour
{
    public Rigidbody2D player_rigid;

    //hook_instruction hook_Instruction;
    //player_move player_Move;
    Rigidbody2D rigidbody_player_object;

    // Start is called before the first frame update
    void Start()
    {
        //hook_Instruction = GetComponent<hook_instruction>();
        //player_Move = GetComponent<player_move>();
        rigidbody_player_object = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody_player_object.velocity += player_rigid.velocity; 
    }
}
