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

        // Tu� bas�ld��� anda z�plamay� ba�lat
        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            StartJump();
        }

        // Tu� bas�l� tutuluyorsa z�plamaya devam et
        if ((Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space)) && isJumping)
        {
            ContinueJump();
        }

        // Tu� b�rak�ld���nda veya maksimum s�re doldu�unda z�plamay� durdur
        if ((Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Space)) && isJumping)
        {
            StopJump();
        }

        animator.SetBool("isRunning", isGrounded);
    }

    private void StartJump()
    {
        isJumping = true;
        jumpTimer = 0f; // Saya� s�f�rlan�r
        isGrounded = false;

        // Ba�lang�� z�plama sesi ve animasyonu
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
            // Yukar�ya kuvvet uygula
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else
        {
            // Maksimum s�re dolunca z�plamay� durdur
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
