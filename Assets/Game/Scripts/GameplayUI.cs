using UnityEngine;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    public string ScoreTextP1 { get => txtScoreP1.text; set { txtScoreP1.text = value; } }
    public string ScoreTextP2 { get => txtScoreP2.text; set { txtScoreP2.text = value; } }

    [SerializeField] private TMP_Text txtScoreP1;
    [SerializeField] private TMP_Text txtScoreP2;

    
}
