using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_playerview : MonoBehaviour
{
    public Transform player_transform;

    private Transform camera_transform;

    // Start is called before the first frame update
    void Start()
    {
        camera_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        camera_transform.position = new Vector3(player_transform.position.x, player_transform.position.y, -10f);
    }
}
