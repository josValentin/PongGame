using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AI_Level
{
    EASY,
    MEDIUM,
    HARD
}

public class PlayerAI : Player
{
    public static AI_Level Level;

    [SerializeField] private float errorRange = 0.02f;
    [SerializeField] private Rigidbody2D rb2dBall;

    float targetPosY;

    int RandomOne() => (Random.value > 0.5f) ? 1 : -1;
    Vector2 refV = Vector2.zero;

    private void Start()
    {
        CameraLimits limits = GameManager.Limits;
        float offset = 2.5f;

        rb2d.position = new Vector2(limits.Right - offset, 0);

        GameManager.P2 = this;

        switch (Level)
        {
            case AI_Level.EASY:
                errorRange = 0.05f;
                break;
            case AI_Level.MEDIUM:
                errorRange = 0.032f;
                break;
            case AI_Level.HARD:
                errorRange = 0.015f;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!GameManager.Playing) { rb2d.velocity = Vector2.zero; return; }

        float velocity_Y = 0;

        CameraLimits limits = GameManager.Limits;

        float posY = rb2d.position.y;
        float sizeOffset = transform.localScale.y / 2;

        //targetPosY = rb2dBall.position.y - errorRange;
        targetPosY = rb2dBall.position.y;

        targetPosY = Mathf.Clamp(targetPosY, limits.Bottom + sizeOffset, limits.Top - sizeOffset);

        velocity_Y = Mathf.Sign(targetPosY - rb2d.position.y) * speed * 2.5f * Time.fixedDeltaTime;
        velocity_Y = Vector2.SmoothDamp(Vector2.zero, new Vector2(0, velocity_Y), ref refV, errorRange).y;

        if (Mathf.Abs(targetPosY - rb2d.position.y) >= velocity_Y)
        {
            rb2d.MovePosition(rb2d.position + new Vector2(0, velocity_Y));
        }
    }

}
