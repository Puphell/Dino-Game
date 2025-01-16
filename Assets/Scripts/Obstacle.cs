using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed = 5f;

    private void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}
