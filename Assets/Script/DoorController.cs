using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Vector2 playerPosition;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private float radiusCheck = 3f;
    [SerializeField] GameObject containerPuzzle;
    [SerializeField] RectTransform uiParent;
    [SerializeField] Transform objectToRotate;
    [SerializeField] GameObject boss;
    Vector2 originPosition;
    private bool isRotating = false;

    private void OnEnable()
    {
        PuzzleGameManager.OnPuzzleCompleted += OpenDoor; 
    }
    private void OnDisable()
    {
        PuzzleGameManager.OnPuzzleCompleted -= OpenDoor;
    }

    private void OpenDoor()
    {
        if (isOpen && !isRotating)
        {
            containerPuzzle.SetActive(false);
            boss.SetActive(true);
            StartCoroutine(RotateObject());
        }
    }


        private void Start()
    {
        originPosition = containerPuzzle.transform.position;
    }

    void Update()
    {
        CheckOpen();

        if (isOpen)
        {
            Vector2 screenPosition = uiParent.transform.position;
            containerPuzzle.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));

            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            Close();
        }
    }

    private void CheckOpen()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isOpen && IsPlayerNearby())
        {
            if (!isOpen)
            {
                isOpen = true;
            }
        }
    }

    private bool IsPlayerNearby()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radiusCheck);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {

                playerPosition = hit.transform.position;
                return true;
            }
        }
        return false;
    }

    private IEnumerator RotateObject()
    {
        isRotating = true;

        Quaternion startRotation = objectToRotate.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 90);
        float duration = 2f;
        float time = 0;

        while (time < duration)
        {
            objectToRotate.rotation = Quaternion.Lerp(startRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        objectToRotate.rotation = endRotation;

        yield return new WaitForSeconds(2f);

        startRotation = objectToRotate.rotation;
        endRotation = startRotation * Quaternion.Euler(0, 0, -90);
        time = 0;

        while (time < duration)
        {
            objectToRotate.rotation = Quaternion.Lerp(startRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        objectToRotate.rotation = endRotation;

        isRotating = false;
    }

    public void Close()
    {
        if (isOpen)
        {
            isOpen = false;
            Time.timeScale = 1f;
            containerPuzzle.transform.position = originPosition;
        }
    }
}
