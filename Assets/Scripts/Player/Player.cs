using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

        #region Variáveis

        [Header("Movimentação")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask groundLayer;

        [Header("Áudio")]
        [SerializeField] private AudioClip attackSound;
        [SerializeField] private AudioClip jumpSound;

        private Health playerHealth;
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidBody;
        private BoxCollider2D boxCollider;
        private Animator animator;
        private PlayerRespawn respawn;

        private bool isGrounded;
        private bool isAttacking;
        private bool forceUnGround;
    #endregion

    private void Awake()
    {
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
            playerHealth = GetComponent<Health>();
    }

        private void Update()
        {
        if (playerHealth.IsDead)
        {
            rigidBody.velocity = Vector2.zero;
            return;
        }


        HandleMovement();
            HandleJump();
            GroundCheck();
            HandleAttack();
            HandleInteract();
        }

        private void HandleMovement()
        {
            float horizontalInput = isAttacking ? 0 : Input.GetAxisRaw("Horizontal");

            rigidBody.velocity = new Vector2(horizontalInput * movementSpeed, rigidBody.velocity.y);

            // Flip do sprite
            if (horizontalInput != 0)
                spriteRenderer.flipX = horizontalInput < 0;

            animator.SetFloat("MoveSpeed", Mathf.Abs(horizontalInput));
            animator.SetBool("IsGrounded", isGrounded);
        }

        private void HandleJump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                SoundManager.Instance.PlaySound(jumpSound);
                animator.SetTrigger("Jump");
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                StartCoroutine(ForceUnGroundTimer());
            }

            if (Input.GetKeyUp(KeyCode.Space) && rigidBody.velocity.y > 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y / 2);
            }
        }

        private void GroundCheck()
        {
            Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red);

            RaycastHit2D hit = Physics2D.BoxCast(
                boxCollider.bounds.center,
                boxCollider.bounds.size,
                0,
                Vector2.down,
                0.1f,
                groundLayer
            );

            isGrounded = hit.collider != null && !forceUnGround;
        }

    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && CanAttack())
        {
            SoundManager.Instance.PlaySound(attackSound);
            animator.SetTrigger("IsAttacking");
            StartCoroutine(AttackLock(0.3f)); // trava por 0.3 segundos, por exemplo
        }
    }

    private IEnumerator AttackLock(float duration)
    {
        isAttacking = true;
        yield return new WaitForSeconds(duration);
        isAttacking = false;
    }

    private bool CanAttack()
        {
            return !isAttacking && isGrounded && Mathf.Abs(rigidBody.velocity.x) == 0;
        }

        private IEnumerator ForceUnGroundTimer()
        {
            forceUnGround = true;
            yield return new WaitForSeconds(0.5f);
            forceUnGround = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IIColectible collectible = collision.GetComponent<IIColectible>();
            collectible?.Collect();
        }

        private void HandleInteract()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                IInteractable interactable = GetComponent<IInteractable>();
                interactable?.Interactable();
            }
        }
    public void SetCheckpoint(Transform checkpoint)
    {
        if (respawn != null)
            respawn.CurrentCheckpoint = checkpoint;
    }
}
    





