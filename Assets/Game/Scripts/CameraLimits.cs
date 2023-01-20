using UnityEngine;

public class CameraLimits
{
    public float Left;
    public float Right;
    public float Top;
    public float Bottom;

    Camera source;

    public CameraLimits(Camera source)
    {
        this.source = source;
        UpdateValues();
    }

    public void UpdateValues()
    {
        Vector2 Right_Top = source.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Right = Right_Top.x;
        Top = Right_Top.y;

        Vector2 Left_Bottom = source.ScreenToWorldPoint(Vector2.zero);
        Left = Left_Bottom.x;
        Bottom = Left_Bottom.y;
    }
}
