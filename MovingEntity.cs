using UnityEngine;

public class MovingEntity : MonoBehaviour
{
    public Vector2 screenSize;
    public Vector2? moveTo = null;

    private float _size = 0f;

    void Start()
    {
        _size = GetComponent<BoxCollider2D>().size.x / 2;
        screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    public Vector2 GetInBoundPosition(Vector2 position)
    {
        var x = position.x;
        var y = position.y;
        x = Mathf.Min(x, screenSize.x - _size);
        x = Mathf.Max(x, -screenSize.x + _size);
        y = Mathf.Min(y, screenSize.y - _size);
        y = Mathf.Max(y, -screenSize.y + _size);

        return new Vector2(x, y);
    }

    public void Move(float speed)
    {
        if (Vector3.Distance(moveTo.Value, transform.position) < Mathf.Epsilon)
        {
            moveTo = null;
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, moveTo.Value, speed * Time.deltaTime);
    }
}
