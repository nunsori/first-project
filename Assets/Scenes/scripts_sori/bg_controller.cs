using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_controller : MonoBehaviour
{
    public Transform player_transform_onbg;
    private new MeshRenderer renderer;
    private Transform bg_transform;
    [SerializeField]
    private float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        bg_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        bg_transform.position = player_transform_onbg.position;
        renderer.material.mainTextureOffset = new Vector2(speed, 0);
    }
}
