using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float jumpForce = 5f;
    public float maxJumpTime = 1f;
    public bool isGrounded = true;

    public AudioSource jumpSound;

    public AudioClip deadSound;

    private float jumpTimer = 0f;
    private bool isJumping = false;
    private bool isDead = false;

    private void Update()
    {
        if (isDead) return;

        // Tuþ basýldýðý anda zýplamayý baþlat
        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            StartJump();
        }

        // Tuþ basýlý tutuluyorsa zýplamaya devam et
        if ((Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space)) && isJumping)
        {
            ContinueJump();
        }

        // Tuþ býrakýldýðýnda veya maksimum süre dolduðunda zýplamayý durdur
        if ((Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Space)) && isJumping)
        {
            StopJump();
        }

        animator.SetBool("isRunning", isGrounded);
    }

    private void StartJump()
    {
        isJumping = true;
        jumpTimer = 0f; // Sayaç sýfýrlanýr
        isGrounded = false;

        // Baþlangýç zýplama sesi ve animasyonu
        if (jumpSound != null)
        {
            jumpSound.Play();
        }
        animator.SetTrigger("Jump");
    }

    private void ContinueJump()
    {
        jumpTimer += Time.deltaTime;

        if (jumpTimer < maxJumpTime)
        {
            // Yukarýya kuvvet uygula
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else
        {
            // Maksimum süre dolunca zýplamayý durdur
            StopJump();
        }
    }

    private void StopJump()
    {
        isJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        jumpSound.PlayOneShot(deadSound);

        animator.SetTrigger("Die");
        rb.gravityScale = 1.5f;
        rb.velocity = Vector2.down * 1.5f;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        Invoke("TriggerGameOver", 2f);
    }

    private void TriggerGameOver()
    {
        FindObjectOfType<GameManager>().GameOver();
    }
}
