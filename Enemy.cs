using UnityEngine;

public class Enemy : MovingEntity
{
    public SettingsScriptableObject settings;

    void Update()
    {
        if (!moveTo.HasValue)
        {
            SetNewMoveTo();
        }
        Move(settings.EnemySpeed);
    }

    private void SetNewMoveTo()
    {
        var newX = Random.Range(-screenSize.x, screenSize.x);
        var newY = Random.Range(-screenSize.y, screenSize.y);

        moveTo = new Vector2(newX, newY);
    }
}
