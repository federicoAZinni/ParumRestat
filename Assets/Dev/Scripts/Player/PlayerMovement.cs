using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dependecies Variables")]
    [SerializeField] Animator anim;
    [SerializeField] CharacterController controller;
    [SerializeField] Transform camera_T;
    public Transform cameraRefToEnemy;

    [Header("Movement Variables")]
    [SerializeField] float moveSpeed;
    [SerializeField] float moveSpeedOnCrouch;
    float speed;
    Vector2 mov;

    [Header("Rotation Variables")]
    [SerializeField, Tooltip("Más es más rápido"), Range(0.1f, 10f)] float rotSpeed;

    public LayerMask layer;
    public bool onCover;
    public bool onCrouch;

    private void Start()
    {
        speed = moveSpeed;
        camera_T = Camera.main.transform;
    }

    private void Update()
    {
        Move();
        Rotation();
        AnimationMovement();
        if (Input.GetKeyDown(KeyCode.Q)) OnCover();
        if (Input.GetKeyDown(KeyCode.C)) OnCrouch();
    }

    private void OnCrouch()
    {
        onCrouch = !onCrouch;
        anim.SetBool("OnCrouch", onCrouch);

        if (onCrouch) 
        { 
            speed = moveSpeedOnCrouch;
            controller.radius = 0.5f;
        }
        else 
        { 
            speed = moveSpeed;
            controller.radius = 0.2f;
        }
    }
    private void OnCrouch(bool temp)
    {
        onCrouch = temp;
        anim.SetBool("OnCrouch", onCrouch);

        if (onCrouch)
        {
            speed = moveSpeedOnCrouch;
            controller.radius = 0.5f;
        }
        else
        {
            speed = moveSpeed;
            controller.radius = 0.2f;
        }
    }

    private IEnumerator CheckIfCanBeOnCover()
    {
        yield return new WaitForSeconds(1);
        while (onCover)
        {
            Vector3 posRay = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
            if (!Physics.Raycast(posRay, -transform.forward, out RaycastHit hit, 1, layer))
            {
                onCover = false;
                OnCover();
            }
            yield return null;
        }
    }
    private void OnCover()
    {
        Vector3 posRay = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);

        if (Physics.Raycast(posRay, transform.forward, out RaycastHit hit, 0.25f, layer))
        {
            OnCrouch(true);
            onCover = true;
            LeanTween.value(gameObject, 0, 1, 1).setOnUpdate((value) => { transform.forward = Vector3.Lerp(transform.forward, hit.normal, value); });
            StartCoroutine(CheckIfCanBeOnCover());
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
                                + transform.right * mov.x);

        controller.Move(moveDirection * speed * Time.deltaTime);
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
