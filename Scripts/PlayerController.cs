using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float minX = -2.5f;
    public float maxX = 2.5f;

    private Vector3 touchStartPos;
    private Vector3 playerStartPos;
    private bool isDragging = false;

    void Update()
    {
#if UNITY_EDITOR
        HandleMouseDrag();
#else
        HandleTouchDrag();
#endif
    }

    // Public wrapper for testing
    public void TestUpdate()
    {
        Update();
    }

    void HandleTouchDrag()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            touchStartPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            playerStartPos = transform.position;
            isDragging = true;
        }
        else if (touch.phase == TouchPhase.Moved && isDragging)
        {
            Vector3 currentTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            float deltaX = currentTouch.x - touchStartPos.x;
            float newX = Mathf.Clamp(playerStartPos.x + deltaX, minX, maxX);
            transform.position = new Vector3(newX, playerStartPos.y, playerStartPos.z);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            isDragging = false;
        }
    }

    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            playerStartPos = transform.position;
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 currentTouch = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            float deltaX = currentTouch.x - touchStartPos.x;
            float newX = Mathf.Clamp(playerStartPos.x + deltaX, minX, maxX);
            transform.position = new Vector3(newX, playerStartPos.y, playerStartPos.z);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
