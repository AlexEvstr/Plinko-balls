using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelPickerButton : MonoBehaviour
{
    private Button levelButton;
    private Image buttonImage;
    private int levelNumber;

    private void Start()
    {
        transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = new Color(0.8f, 0.7f, 0);
        CheckLevel();
        levelButton.onClick.AddListener(ChooseLevel);
    }

    private void CheckLevel()
    {
        levelButton = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        if (levelButton == null || buttonImage == null || !int.TryParse(gameObject.name, out levelNumber))
        {
            return;
        }

        int highestUnlockedLevel = PlayerPrefs.GetInt("bestLevel", 1);
        if (highestUnlockedLevel < levelNumber)
        {
            levelButton.enabled = false;
            transform.GetChild(1).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            //buttonImage.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            levelButton.enabled = true;
            buttonImage.color = new Color(1f, 1f, 1f);
            if (transform.childCount > 1)
            {
                transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    public void ChooseLevel()
    {
        StartCoroutine(BehaviorBeforeStart());
    }

    private IEnumerator BehaviorBeforeStart()
    {
        transform.localScale = new Vector2(1.15f, 1.15f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector2(1f, 1f);
        yield return new WaitForSeconds(0.1f);
        if (int.TryParse(gameObject.name, out levelNumber))
        {
            PlayerPrefs.SetInt("currentLevel", levelNumber);
            SceneManager.LoadScene("GameScene");
        }
    }
}
