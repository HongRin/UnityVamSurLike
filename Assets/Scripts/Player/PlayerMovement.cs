using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    public float RotateSpeed = 10.0f;
    public Vector3 CameraOffset = new Vector3();

    private Camera Camera;
    private CharacterController CharacterController;
    private Animator Animator;

    private Vector3 MoveDir;

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        Camera = Camera.main;
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        Animate();
        FollowCamera();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        MoveDir = new Vector3(horizontal, 0, vertical).normalized;

        CharacterController.SimpleMove(MoveDir * MoveSpeed);
    }

    private void Rotate()
    {
        if (MoveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(MoveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, RotateSpeed * Time.deltaTime);
        }
    }

    private void Animate()
    {
        Animator.SetFloat("Speed", MoveDir.magnitude);
    }

    private void FollowCamera()
    {
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, transform.position + CameraOffset, 2.0f * Time.fixedDeltaTime);
    }
}
