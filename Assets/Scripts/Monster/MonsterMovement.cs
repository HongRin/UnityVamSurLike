using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform Target;
    public float MoveSpeed = 3.0f;
    public float RotateSpeed = 10.0f;


    private Rigidbody Rigidbody;
    private Animator Animator;
    private bool IsSpawned = false;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator  = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (Target == null) return;

        Move();
    }

    public void Initialize(Transform player)
    {
        Target = player;
        StartCoroutine(SpawnStartCoroutine(transform.localScale));
    }

    IEnumerator SpawnStartCoroutine(Vector3 scaleEnd)
    {
        Vector3 scaleStart = Vector3.zero;
        float duration = 0.5f;
        float timer = 0.0f;

        while (timer < duration)
        {
            float t = timer / duration;
            transform.localScale = Vector3.Lerp(scaleStart, scaleEnd, t);
            timer += Time.deltaTime;
            yield return null;
        }

        IsSpawned = true;

        Animator.SetTrigger("Move");
    }

    public void Move()
    {
        Vector3 direction = (Target.position - transform.position).normalized;

        direction.y = 0f;
        float distance = direction.magnitude;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotateSpeed * Time.fixedDeltaTime);
        }

        Rigidbody.MovePosition(Rigidbody.position + direction * MoveSpeed * Time.fixedDeltaTime);
    }
}
