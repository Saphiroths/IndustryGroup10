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
    public Animator anim;


    //Player Variables
    [Header("Variables")]
    public float moveSpeed;
    public float baseMoveSpeed = 5f;
    public bool m_FacingRight;

    //Vectors
    [Header("Vectors")]
    public Vector2 movement;
    public Vector2 mousePos;
    public Vector3 lastMoveDir;

    private void OnEnable()
    {
        moveSpeed = baseMoveSpeed;

        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer == null)
        {
            Debug.LogError("No Sprite");
        }
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement.normalized);
    }
}

