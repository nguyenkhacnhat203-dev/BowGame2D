using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 8f;
    public float flyOutSpeed = 6f;
    public float rotateSpeed = 720f; 

    private Rigidbody2D rb;

    private bool isFlying = false;
    private bool isStuck = false;
    private bool isFlyOut = false;

    private Vector2 flyOutDir;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    void Update()
    {
        if (isFlyOut)
        {
            transform.position += (Vector3)(flyOutDir * flyOutSpeed * Time.deltaTime);
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
    }

    public void Shoot()
    {
        if (isFlying || isStuck || isFlyOut) return;

        isFlying = true;
        rb.isKinematic = false;
        rb.velocity = Vector2.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStuck || isFlyOut) return;

        if (collision.gameObject.CompareTag("Wood"))
        {
            LevelManager.Instance.remainingArrows--;
            StickToWood(collision.transform);
            LevelManager.Instance.CountBowHit++;
            LevelManager.Instance.UpdateTargetArrowText();

        }
        else if (collision.gameObject.CompareTag("Arrow"))
        {
            LevelManager.Instance.remainingArrows--;
            Vector2 incomingVelocity = rb.velocity;
            Vector2 hitNormal = collision.contacts[0].normal;
            FlyOut(incomingVelocity, hitNormal);

        }
    }

    void StickToWood(Transform wood)
    {
        isStuck = true;
        isFlying = false;

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        transform.SetParent(wood);
    }

    void FlyOut(Vector2 incomingVelocity, Vector2 hitNormal)
    {

        isFlyOut = true;
        isFlying = false;

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        transform.SetParent(null);

        flyOutDir = Vector2.Reflect(incomingVelocity.normalized, hitNormal);

        flyOutDir += Random.insideUnitCircle * 0.08f;
        flyOutDir.Normalize();
        gameObject.GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 3f);
    }
}
