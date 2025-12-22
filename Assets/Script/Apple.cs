using UnityEngine;

public class Apple : MonoBehaviour
{
    [Header("Apple Visual")]
    public GameObject Apple1;
    public GameObject Apple2;

    [Header("Fly Out Setting")]
    public float flyOutSpeed = 5f;
    public float rotateSpeed = 720f;

    private Rigidbody2D rb;
    private bool isFlyOut = false;
    private Vector2 flyOutDir;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        if (Apple1 != null) Apple1.SetActive(true);
        if (Apple2 != null) Apple2.SetActive(false);
    }

    void Update()
    {
        if (isFlyOut)
        {
            transform.position += (Vector3)(flyOutDir * flyOutSpeed * Time.deltaTime);
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isFlyOut) return;

        if (other.CompareTag("Arrow"))
        {
            Rigidbody2D arrowRb = other.GetComponent<Rigidbody2D>();
            if (arrowRb == null) return;
            LevelManager.Instance.CountAppleHit++;
            LevelManager.Instance.UpdateTargetAppleText();
            if (Apple1 != null) Apple1.SetActive(false);
            if (Apple2 != null) Apple2.SetActive(true);
            gameObject.GetComponent<Collider2D>().enabled = false;  
            Vector2 incomingVelocity = arrowRb.velocity;
            Vector2 hitDir = incomingVelocity.normalized;

            FlyOut(hitDir);
        }
    }

    void FlyOut(Vector2 direction)
    {
        isFlyOut = true;

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        transform.SetParent(null);

        flyOutDir = direction;
        flyOutDir += Random.insideUnitCircle * 0.1f;
        flyOutDir.Normalize();

        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 3f);
    }
}
