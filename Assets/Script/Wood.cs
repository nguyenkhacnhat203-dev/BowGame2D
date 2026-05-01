using UnityEngine;
using System.Collections;

public class Wood : MonoBehaviour
{
    public enum RotateCase
    {
        Case1, // Trái -> Phải cố định
        Case2, // Phải -> Trái cố định
        Case3, // Quay 1 vòng rồi đổi chiều
        Case4, // 3s đổi chiều quay + speed random
        Case5, // 2–5s đổi chiều quay + speed random
        Case6 // mỗi 2-5s xoay nhanh trong khoảng thời gian random 0.2-1s 
    }

    [Header("Rotate Setting")]
    public RotateCase rotateCase;
    public float speed = 60f;

    private float currentSpeed;
    private int direction = 1;
    private float rotatedAngle = 0f;

    [Header("Hit Reaction")]
    public float pushDistance = 0.3f;
    public float pushDuration = 0.15f;

    private Vector3 originalPos;
    private Coroutine pushCoroutine;

    void Start()
    {
        originalPos = transform.position;
        currentSpeed = speed;

        switch (rotateCase)
        {
            case RotateCase.Case1:
                direction = 1;
                break;

            case RotateCase.Case2:
                direction = -1;
                break;

            case RotateCase.Case3:
                direction = -1;
                break;

            case RotateCase.Case4:
                StartCoroutine(ChangeDirectionEvery3s());
                break;

            case RotateCase.Case5:
                StartCoroutine(RandomDirectionRandomTime());
                break;
            case RotateCase.Case6:
                StartCoroutine(RandomSpeedRandomTime());
                break;
        }
    }

    void Update()
    {
        switch (rotateCase)
        {
            case RotateCase.Case1:
            case RotateCase.Case2:
                RotateWood(direction, speed);
                break;

            case RotateCase.Case3:
                RotateCase3();
                break;

            case RotateCase.Case4:
            case RotateCase.Case5:
                RotateWood(direction, currentSpeed);
                break;
            case RotateCase.Case6:
                RotateWood(direction, currentSpeed);
                break;
        }
    }

    void RotateWood(int dir, float spd)
    {
        transform.Rotate(0f, 0f, dir * spd * Time.deltaTime);
    }

    void RotateCase3()
    {
        float delta = speed * Time.deltaTime;
        transform.Rotate(0f, 0f, direction * delta);
        rotatedAngle += delta;

        if (rotatedAngle >= 360f)
        {
            rotatedAngle = 0f;
            direction *= -1;
        }
    }

    IEnumerator ChangeDirectionEvery3s()
    {
        while (true)
        {
            direction *= -1;
            currentSpeed = Random.Range(speed, speed * 2f);
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator RandomDirectionRandomTime()
    {
        while (true)
        {
            direction = Random.value > 0.5f ? 1 : -1;
            currentSpeed = Random.Range(speed, speed * 2.5f);
            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

    IEnumerator RandomSpeedRandomTime()
    {
        while (true)
        {
            currentSpeed = Random.Range(speed * 2, speed * 3f);
            yield return new WaitForSeconds(Random.Range(0.2f, 1f));
            currentSpeed = speed;
            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            if (pushCoroutine != null)
                StopCoroutine(pushCoroutine);

            pushCoroutine = StartCoroutine(PushUpAndBack());
        }
    }

    IEnumerator PushUpAndBack()
    {
        Vector3 startPos = transform.position;
        Vector3 pushPos = startPos + Vector3.up * pushDistance;

        float t = 0f;

        // Đẩy lên
        while (t < pushDuration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, pushPos, t / pushDuration);
            yield return null;
        }

        t = 0f;

        // Quay về vị trí cũ
        while (t < pushDuration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(pushPos, startPos, t / pushDuration);
            yield return null;
        }

        transform.position = startPos;
    }
}
