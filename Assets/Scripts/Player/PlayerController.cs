using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    #region Movement Config
    [Header("Movimentação")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    #endregion

    #region Áudio
    [Header("Áudio")]
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip jumpSound;
  
    #endregion

    #region Components
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private PlayerRespawn respawn;
    private PlayerHealth playerHealth;
    #endregion

    #region States
    private bool isGrounded;
    private bool isAttacking;
    private bool forceUnGround;
    #endregion

    #region Eventos
    public event Action OnJump;
    public event Action OnAttack;
    public event Action OnLand;
    #endregion

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerHealth = GetComponent<PlayerHealth>();
        respawn = GetComponent<PlayerRespawn>();
        
    }

    private void Update()
    {
        

        GroundCheck();
        HandleMovement();
        HandleJump();
        HandleAttack();
        
    }

    #region Movimento
    private void HandleMovement()
    {
        float horizontalInput = isAttacking ? 0 : Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);

        if (horizontalInput != 0)
            spriteRenderer.flipX = horizontalInput < 0;

        animator.SetFloat("MoveSpeed", Mathf.Abs(horizontalInput));
        animator.SetBool("IsGrounded", isGrounded);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
            SoundManager.instance?.PlaySound(jumpSound);
            OnJump?.Invoke();
            StartCoroutine(ForceUnGroundTimer());
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }
    }

    private IEnumerator ForceUnGroundTimer()
    {
        forceUnGround = true;
        yield return new WaitForSeconds(0.5f);
        forceUnGround = false;
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        bool wasGrounded = isGrounded;
        isGrounded = hit.collider != null && !forceUnGround;

        if (!wasGrounded && isGrounded)
            OnLand?.Invoke();

        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red);
    }
    #endregion

    #region Combate
    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && CanAttack())
        {
            isAttacking = true;
            animator.SetTrigger("IsAttacking");
            SoundManager.instance?.PlaySound(attackSound);
            OnAttack?.Invoke();
            StartCoroutine(AttackCooldown(0.3f));
        }
    }

    private bool CanAttack()
    {
        return !isAttacking && isGrounded && Mathf.Abs(rb.velocity.x) == 0;
    }

    private IEnumerator AttackCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        isAttacking = false;
    }
    #endregion

   
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IIColectible collectible = collision.GetComponent<IIColectible>();
        if (collectible != null)
        {
            collectible.Collect();
        }
      
    }
    

    #region Respawn/Checkpoint
    public void SetCheckpoint(Transform checkpoint)
    {
        if (respawn != null)
            respawn.CurrentCheckpoint = checkpoint;

    }
    #endregion

   



}
    


