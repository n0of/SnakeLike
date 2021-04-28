using UnityEngine;

public class Player : MovingEntity
{
    public SettingsScriptableObject settings;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            moveTo = GetInBoundPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (!moveTo.HasValue)
        {
            return;
        }
        Move(settings.PlayerSpeed);
    }
}
