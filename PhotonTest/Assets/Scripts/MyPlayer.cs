using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MyPlayer : MonoBehaviour, IPunObservable
{
    public PhotonView photonView;
    private int myPVID;
    private int enemyPVID;

    public float moveSpeed = 4f;
    private Vector3 smoothMove;
    private Animator _anim;
    private Rigidbody2D _rigidbody;
    [SerializeField] private HUD playerHUD;
    private GameObject sceneCamera;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float hitDelay = .4f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    private float _jumpTimeCounter;
    private bool _isJumping;
    [SerializeField] private bool onGround;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = .5f;

    private int health = 3;
    void Start()
    {
        if(photonView.IsMine == false)
        {
            playerCamera.SetActive(false);
        }
        _anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        playerHUD = GameObject.FindGameObjectWithTag("HUD_" + this.gameObject.tag).GetComponent<HUD>(); 
        playerHUD.gameObject.GetComponent<Canvas>().worldCamera = playerCamera.gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        if(photonView.IsMine)
        {
            ProcessInputs();
        }
        else
        {
            SmoothMovement();
        }


    }

    private void ProcessInputs()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        _anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        Vector2 velocity = _rigidbody.velocity;
        velocity.x = Input.GetAxis("Horizontal") * moveSpeed;
        _rigidbody.velocity = velocity;

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

        if(Input.GetButtonDown("Fire1"))
        {
            _anim.SetTrigger("Attack");
            Invoke("Attack", hitDelay);
        }

        if(onGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            _anim.SetBool("isJump", true);
            _isJumping = true;
            _jumpTimeCounter = jumpTime;
            velocity.x = Input.GetAxis("Horizontal") * moveSpeed;
            velocity.y =  (Vector2.up * jumpForce).y;
            _rigidbody.velocity = new Vector2(velocity.x, velocity.y);
        }

        if(Input.GetKey(KeyCode.Space) && _isJumping == true)
        {
            if(_jumpTimeCounter > 0f)
            {
                velocity.x = Input.GetAxis("Horizontal") * moveSpeed;
                velocity.y =  (Vector2.up * jumpForce).y;
                _rigidbody.velocity = new Vector2(velocity.x, velocity.y);
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
            _anim.SetBool("isJump", false);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(health);
        }
        else if(stream.IsReading)
        {
            smoothMove = (Vector3)stream.ReceiveNext();
            health = (int)stream.ReceiveNext();
        }
    }

    private void SmoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, smoothMove, 0.1f);
    }

    public void UpdateCrystall()
    {
        playerHUD.UpdateCrystall();
    }

    public void Attack()
    {
        DoHit(attackPoint.position, attackRange);
    }

    private void DoHit(Vector3 hitPosition, float hitRadius)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(hitPosition, hitRadius);

        for(int i = 0; i < hits.Length; i++)
        {
            if(!GameObject.Equals(hits[i].gameObject, this.gameObject))
            {
                MyPlayer player = hits[i].gameObject.GetComponent<MyPlayer>();
                if(player != null)
                {
                    player.photonView.RPC("RecieveHit", RpcTarget.Others);
                    break;
                }
            }
        }  
    }

    [PunRPC]
    public void RecieveHit()
    {
        health--;

        Debug.Log(this.gameObject.name + " get hit " + photonView.IsMine);

        _anim.SetTrigger("GetHit");
        
        playerHUD.UpdateLives(health);

        if(health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _anim.SetTrigger("Die");
    }
}