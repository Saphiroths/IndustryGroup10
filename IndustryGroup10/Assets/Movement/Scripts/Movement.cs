using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Player objects
    [Header("Objects")]
    public Rigidbody2D rb;
    public SpriteRenderer _renderer;
    public Camera cam;
    //public Animator anim;


    //Player Variables
    [Header("Variables")]
    public float moveSpeed;
    public float baseMoveSpeed = 5f;

    //Vectors
    [Header("Vectors")]
    public Vector2 movement;

    private void OnEnable()
    {
        moveSpeed = baseMoveSpeed;

        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer == null)
        {
            Debug.LogError("No Sprite");
        }
    }

    void FixedUpdate()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement.normalized);
    }
}

