using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static CameraLimits Limits { get; private set; }
    public static bool Playing;
    public static Player P1;
    public static Player P2;
    public static bool UsingAI = true;

    public static SoundEffectts Sound { get => Instance.sound; }
    [SerializeField] private SoundEffectts sound;
    [SerializeField] private GameplayUI gameplayUI;

    private int scoreP1, scoreP2;


    private void Awake()
    {
        Application.targetFrameRate = 60;
        Instance = this;
        Limits = new CameraLimits(Camera.main);
    }

    private void Update()
    {
        Limits.UpdateValues();

        if (Input.GetKeyDown(KeyCode.Escape))
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public static float GetHDistance() => Limits.Right - Limits.Left;

    public void AddScoreP1()
    {
        scoreP1++;
        gameplayUI.ScoreTextP1 = scoreP1.ToString();
        sound.PlayScore();
    }

    public void AddScoreP2()
    {
        scoreP2++;
        gameplayUI.ScoreTextP2 = scoreP2.ToString();
        sound.PlayScore();
    }
}
