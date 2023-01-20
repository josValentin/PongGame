using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private bool isPlayer1;
    [SerializeField] protected Rigidbody2D rb2d;
    [SerializeField] protected float speed = 10;
    float dir;
    float offset = 2.5f;

    private void Start()
    {
        SetHPos();

        if (isPlayer1)        
            GameManager.P1 = this;      
        else
        {
            if (GameManager.UsingAI)
            {
                enabled = false;
                GetComponent<PlayerAI>().enabled = true;
            }
            else
                GameManager.P2 = this;
        }
    }

    void SetHPos()
    {
        CameraLimits limits = GameManager.Limits;

        if (isPlayer1)
            rb2d.position = new Vector2(limits.Left + offset, rb2d.position.y);
        else
            rb2d.position = new Vector2(limits.Right - offset, rb2d.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        dir = GetInput();
        SetHPos();
    }

    float GetInput()
    {
        KeyCode UpInput = isPlayer1 ? KeyCode.W : KeyCode.UpArrow;
        KeyCode DownInput = isPlayer1 ? KeyCode.S : KeyCode.DownArrow;

        if (Input.GetKey(UpInput)) return 1;
        else if (Input.GetKey(DownInput)) return -1;

        return 0;
    }

    private void FixedUpdate()
    {
        float velocity_Y = 0;

        CameraLimits limits = GameManager.Limits;

        float posY = rb2d.position.y;
        float sizeOffset = transform.localScale.y / 2;

        bool isOnLimit = (posY >= limits.Top - sizeOffset && dir > 0) || (posY <= limits.Bottom + sizeOffset && dir < 0);

        if (!isOnLimit) velocity_Y = dir * speed * 2.5f * Time.fixedDeltaTime;

        rb2d.MovePosition(rb2d.position + new Vector2(0, velocity_Y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Sound.PlayPaddle();

        if (!Ball.UsingLaunchSpeed) return;
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null) ball.UpdateBallSpeed();
    }

}
