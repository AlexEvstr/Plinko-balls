using UnityEngine;

public class FieldActivator : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    private int currentActiveObjectIndex = -1;
    private LevelController levelController;

    void Start()
    {
        levelController = GetComponent<LevelController>();
        int currentLevel = levelController.CurrentLevel;

        // Проверяем, был ли этот уровень посещен ранее
        bool hasEnteredBefore = PlayerPrefs.GetInt("HasEnteredLevel_" + currentLevel, 0) == 1;

        if (!hasEnteredBefore)
        {
            // Впервые входим в этот уровень, активируем следующий объект
            ActivateNextObject(currentLevel);
            PlayerPrefs.SetInt("HasEnteredLevel_" + currentLevel, 1);
        }
        else
        {
            // Уровень уже был посещен, активируем тот же объект
            currentActiveObjectIndex = PlayerPrefs.GetInt("ActiveObjectIndex_" + currentLevel, -1);
            if (currentActiveObjectIndex >= 0 && currentActiveObjectIndex < objectsToActivate.Length)
            {
                ActivateObject(currentActiveObjectIndex);
            }
        }

        PlayerPrefs.Save();
    }

    void ActivateNextObject(int level)
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
        }

        // Получаем индекс активного объекта для текущего уровня
        currentActiveObjectIndex = PlayerPrefs.GetInt("LastActiveObjectIndex", -1);
        currentActiveObjectIndex = (currentActiveObjectIndex + 1) % objectsToActivate.Length;

        objectsToActivate[currentActiveObjectIndex].SetActive(true);

        PlayerPrefs.SetInt("ActiveObjectIndex_" + level, currentActiveObjectIndex);
        PlayerPrefs.SetInt("LastActiveObjectIndex", currentActiveObjectIndex);
    }

    void ActivateObject(int index)
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
        }

        objectsToActivate[index].SetActive(true);
    }
}