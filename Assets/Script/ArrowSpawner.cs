using UnityEngine;
using System.Collections;

public class ArrowSpawner : MonoBehaviour
{
    [Header("References")]
    public Arrow arrowPrefab;
    public Transform spawnPoint;

    [Header("Arrow Setting")]
    public int CountArrow = 10;

    private Arrow currentArrow;
    public bool canFire = true;

    private void Start()
    {
        SpawnArrow();
    }

    private void Update()
    {
        CheckFire();
    }




    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(0.1f);
        FireArrow();

    }

    private void CheckFire()
    {

        if (!canFire) return;
        if (currentArrow == null) return;
        if (CountArrow <= 0) return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            StartCoroutine(ShootDelay());

        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ShootDelay());


        }
    }

    private void FireArrow()
    {
        if (PopupManager.Instance.isShowPopup == true) return;

        if (!canFire || currentArrow == null || CountArrow <= 0) return;

        canFire = false;

        currentArrow.Shoot();
        currentArrow = null;

        CountArrow--;
        LevelManager.Instance.UpdateArrowText();

        Invoke(nameof(SpawnArrow), 0.05f);
    }

    private void SpawnArrow()
    {
        if (CountArrow <= 0) return;
        currentArrow = Instantiate(
            arrowPrefab,
            spawnPoint.position,
            Quaternion.identity,
            transform
        );
    
        canFire = true;
    }

    public void AddArrow(int amount)
    {
        bool wasEmpty = CountArrow <= 0;

        CountArrow += amount;
        LevelManager.Instance.remainingArrows += amount;
        if (wasEmpty && currentArrow == null)
        {
            SpawnArrow();
        }
    }
}
