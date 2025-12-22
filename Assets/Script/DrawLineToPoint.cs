using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class DrawLineToPoint : MonoBehaviour
{
    public Vector3 targetPositionA;
    public Vector3 targetPositionB;
    public ArrowSpawner arrowSpawner;
    private float backDelay = 0.15f;

    private LineRenderer line;
    private bool canTouch = true;

    void Awake()
    {
        line = GetComponent<LineRenderer>();

        line.positionCount = 2;

        line.startColor = Color.white;
        line.endColor = Color.white;

        line.startWidth = 0.08f;
        line.endWidth = 0.08f;

        line.numCapVertices = 8;
        line.numCornerVertices = 8;

        line.useWorldSpace = true;
        line.alignment = LineAlignment.TransformZ;
        line.textureMode = LineTextureMode.Stretch;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, targetPositionA);
    }

    void Update()
    {

        CheckDraw();
    }



    private void CheckDraw()
    {
        line.SetPosition(0, transform.position);

        if (!canTouch) return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            StartCoroutine(LineToBThenBack());
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(LineToBThenBack());
        }
    }

    IEnumerator LineToBThenBack()
    {


        canTouch = false;

        yield return new WaitForSeconds(backDelay / 2f);
        if (PopupManager.Instance.isShowPopup == true || arrowSpawner.CountArrow == 0)
            yield break;
        line.SetPosition(1, targetPositionB);

        yield return new WaitForSeconds(backDelay / 2f);

        line.SetPosition(1, targetPositionA);

        canTouch = true;
    }

}
