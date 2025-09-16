using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dependecies Variables")]
    [SerializeField] Animator anim;
    [SerializeField] CharacterController controller;
    [SerializeField] Transform camera_T;

    [Header("Movement Variables")]
    [SerializeField] float moveSpeed;
    Vector2 mov;

    [Header("Rotation Variables")]
    [SerializeField,Tooltip("Más es más rápido"), Range(0.1f, 10f)] float rotSpeed;

    public LayerMask layer;

    public bool onCover;
    private void Update()
    {
        Move();
        Rotation();
        AnimationMovement();
        OnCover();
    }


    private void OnCover()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;
        
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1, layer))
        {
            transform.forward = hit.normal;
            onCover = true;
        }
        else
            onCover = false;
        
        anim.SetBool("OnCover", onCover);
    }

    private void Move()
    {
        mov = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 moveDirection = Vector3.zero;

        if (onCover) moveDirection =  (-transform.forward * 0 +(transform.right * -mov.x)).normalized;
        else moveDirection = (transform.forward * mov.y
                                + transform.right * mov.x).normalized;

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void Rotation()
    {
        if (onCover) return;
        if (controller.velocity.magnitude <= 0) return;
        Quaternion newRot = Quaternion.LookRotation(camera_T.forward);
        newRot.x = transform.rotation.x;
        newRot.z = transform.rotation.z;
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot,Time.deltaTime * rotSpeed);
    }

    void AnimationMovement()
    {
        anim.SetFloat("speedH", mov.y);
        anim.SetFloat("speedV", mov.x);
        anim.SetFloat("Speed",controller.velocity.magnitude);
    }
}
