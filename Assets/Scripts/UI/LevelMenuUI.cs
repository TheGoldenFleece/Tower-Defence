using UnityEngine;
using UnityEngine.UI;

public class LevelMenuUI : MonoBehaviour
{
    [SerializeField] SceneFader sceneFader; 
    [SerializeField] GameObject levels;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

        for (int i = 0; i < levels.transform.childCount; i++)
        {
            if (i + 1 > levelReached)
            {
                LockLevel(levels.transform.GetChild(i).gameObject);
            }
        }
    }

    void LockLevel(GameObject button)
    {
        Button _button = button.GetComponent<Button>();
        _button.interactable = false;
        button.transform.GetChild(0).gameObject.SetActive(false);
        button.transform.GetChild(1).gameObject.SetActive(true);
    }
    public void Select(string levelName)
    {
        sceneFader.FadeTo(levelName);
    }
    public void MainMenu()
    {
        sceneFader.FadeTo("MainMenu");
    }
}
