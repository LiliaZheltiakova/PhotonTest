using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Creature
{
    [SerializeField] private float stairSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    private float _jumpTimeCounter;
    private bool _isJumping;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = .5f;

    [SerializeField] private float hitDelay = .4f;

    [SerializeField] private bool onGround;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        
        m_anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        Vector2 velocity = m_rigidbody.velocity;
        velocity.x = Input.GetAxis("Horizontal") * m_speed;
        m_rigidbody.velocity = velocity;

        if(this.transform.localScale.x < 0f)
        {
            if(Input.GetAxis("Horizontal") > 0f)
                this.transform.localScale = Vector3.one;
        }

        else
        {
            if(Input.GetAxis("Horizontal") < 0f)
                this.transform.localScale = new Vector3(-1f, 1f, 0f);
        }

        if(Input.GetButtonDown("Fire1"))// && canAttack)
        {
            m_anim.SetTrigger("Attack");
            //Invoke("Attack", hitDelay);
            //GameController.instance.AudioManager.PlaySound("DM-CGS-47");
        }

        if(onGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            m_anim.SetBool("isJump", true);
            _isJumping = true;
            _jumpTimeCounter = jumpTime;
            m_rigidbody.velocity = Vector2.up * jumpForce;
        }

        if(Input.GetKey(KeyCode.Space) && _isJumping == true)
        {
            if(_jumpTimeCounter > 0f)
            {
                m_rigidbody.velocity = Vector2.up * jumpForce;
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            _isJumping = false;
            m_anim.SetBool("isJump", false);
        }
    }
}
