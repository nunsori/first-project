using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class joy_stick_control : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler , IPointerDownHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10f,150f)]
    private float leverRange;

    public Transform player_transform;
    public Vector3 Hook_dir_transform;

    public hook_instruction hook_Instruction;

    private float distance;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (hook_Instruction.isAttach == true)
        {
            hook_Instruction.eject_wire();
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;


        lever.anchoredPosition = clampedDir;

        if(hook_Instruction.isAttach == true)
        {
            hook_Instruction.eject_wire();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;

        lever.anchoredPosition = clampedDir;

        if (hook_Instruction.isAttach == true)
        {
            hook_Instruction.eject_wire();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (hook_Instruction.isAttach == false)
        {
            distance = Vector2.Distance(lever.transform.position, rectTransform.transform.position);
            Debug.Log("distance is" + distance);

            hook_Instruction.max_distance = distance * 0.06f;
            caculation_angle();
            hook_Instruction.hook_shoot();
        }
        else if (hook_Instruction.isAttach == true)
        {
            hook_Instruction.eject_wire();
        }
        lever.anchoredPosition = Vector2.zero;

    }

    public void caculation_angle()
    {
        Vector2 v1 = lever.position - rectTransform.position;

        float angle = Mathf.Atan2(v1.y, v1.x);

        //Hook_dir_transform.position.y = v1.y + player_transform.position.y;

        Hook_dir_transform = new Vector3(v1.x+player_transform.position.x, v1.y+player_transform.position.y,0);
    }
}
