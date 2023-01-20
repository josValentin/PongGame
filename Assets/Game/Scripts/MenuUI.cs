using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    int current;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (current == 0) return;

            current--;
            SwitchPanel(panels[current]);
        }
    }

    public void SwitchPanel(GameObject targetPanel)
    {
        if (targetPanel == null) return;

        for (int i = 0; i < panels.Length; i++)
        {
            GameObject panel = panels[i];
            if (panel.Equals(targetPanel))
            {
                panel.SetActive(true);
                current = i;
                continue;
            }

            panel.SetActive(false);
        }
    }

    public void UsingAI(bool value) => GameManager.UsingAI = value;

    public void SetAILevel(int level) => PlayerAI.Level = (AI_Level)level;

    public void LoadGame() => SceneManager.LoadScene("Game");


    public void ExitGame() => Application.Quit();
}
