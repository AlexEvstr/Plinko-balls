using UnityEngine;

public class FieldColor : MonoBehaviour
{
    [SerializeField]
    private Color[] colors;

    [SerializeField] private LevelController levelController;

    void Start()
    {
        int currentLevel = levelController.CurrentLevel;

        Color assignedColor = GetAssignedColor(currentLevel);

        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child == this.transform)
                continue;

            AssignColor(child.gameObject, assignedColor);
        }
    }

    Color GetAssignedColor(int level)
    {
        int colorIndex = (level - 1) % colors.Length;
        return colors[colorIndex];
    }

    void AssignColor(GameObject obj, Color color)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }
}
