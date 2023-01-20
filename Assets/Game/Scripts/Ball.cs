using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallState
{
    FirstLaunch,
    GoP1,
    GoP2
}

public class Ball : MonoBehaviour
{
    public static bool UsingLaunchSpeed = true;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    [SerializeField] float minXSpeed;
    [SerializeField] private float launchSpeed;
    [SerializeField] int minAngle = 0, maxAngle = 70;

    CameraLimits limits;
    //Vector2[] directions = new Vector2[] { Vector2.one, Vector2.one * -1, new Vector2(1, -1), new Vector2(-1, 1) };

    float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        limits = GameManager.Limits;
        InitializeSpeeds();
        Restart(BallState.FirstLaunch);
    }

    float m_speed;
    float m_minXSpeed;
    float m_launchSpeed;

    private void Update()
    {
        InitializeSpeeds();
    }

    void InitializeSpeeds()
    {
        float dist = GameManager.GetHDistance();
        float divider = 50;
        m_speed = dist / divider * speed;
        m_minXSpeed = dist / divider * minXSpeed;
        m_launchSpeed = dist / divider * launchSpeed;
    }

    int RandomOne() => (Random.value > 0.5f) ? 1 : -1;

    IEnumerator InitBall(BallState state, float delay = 0)
    {
        yield return new WaitForSeconds(delay);

        GameManager.Playing = true;
        UsingLaunchSpeed = true;

        rb2d.position = Vector2.zero;

        //rb2d.velocity = directions[Random.Range(0, 4)] * speed;
        Vector2 dir = Vector2.zero;
        switch (state)
        {
            case BallState.FirstLaunch:
                float x = RandomOne();
                float y = Mathf.Sin(Random.Range(minAngle, maxAngle)) * RandomOne();
                dir = new Vector2(x, y);
                break;
            case BallState.GoP1:
                dir = ((Vector2)GameManager.P1.transform.position - Vector2.zero).normalized;
                break;
            case BallState.GoP2:
                dir = ((Vector2)GameManager.P2.transform.position - Vector2.zero).normalized;
                break;
            default:
                break;
        }


        currentSpeed = m_launchSpeed;
        rb2d.velocity = dir;
    }
    public void UpdateBallSpeed()
    {
        currentSpeed = m_speed;
        UsingLaunchSpeed = false;
    }

    void Restart(BallState state)
    {
        GameManager.Playing = false;
        StartCoroutine(InitBall(state, 0.8f));
    }

    private void FixedUpdate()
    {
        if (!GameManager.Playing) return;

        Vector2 velocity = rb2d.velocity;

        if (rb2d.position.y > limits.Top - 0.5f || rb2d.position.y < limits.Bottom + 0.5f)
        {
            GameManager.Sound.PlayWall();
            velocity.y = -velocity.y;
        }

        velocity = velocity.normalized * currentSpeed * 100 * Time.fixedDeltaTime;

        if (!UsingLaunchSpeed)
        {
            float minXVelocity = m_minXSpeed * 100 * Time.fixedDeltaTime;

            if (Mathf.Abs(velocity.x) < minXVelocity) velocity.x = minXVelocity * Mathf.Sign(velocity.x);
        }

        rb2d.velocity = velocity;

        if (rb2d.position.x > limits.Right + 0.5f) // Win P1
        {
            GameManager.Instance.AddScoreP1();
            Restart(BallState.GoP2);
        }
        else if (rb2d.position.x < limits.Left - 0.5f) // Win P2
        {
            GameManager.Instance.AddScoreP2();
            Restart(BallState.GoP1);
        }
    }
}
