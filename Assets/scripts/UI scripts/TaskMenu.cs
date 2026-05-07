using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskMenu : MonoBehaviour
{
    // Name of your task panel in the Hierarchy
    private string taskPanelName = "TaskPanel";

    private GameObject taskPanel;
    private bool isOpen = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-find the task panel in the new scene
        isOpen = false;
        FindTaskPanel();
    }

    void Start()
    {
        FindTaskPanel();
    }

    void FindTaskPanel()
    {
        taskPanel = GameObject.Find(taskPanelName);

        if (taskPanel != null)
            taskPanel.SetActive(false);
    }

    void Update()
    {
        // Press I to open/close the task menu
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (taskPanel == null)
                FindTaskPanel();

            if (taskPanel == null) return; // no panel in this scene

            isOpen = !isOpen;
            taskPanel.SetActive(isOpen);

            Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isOpen;
        }
    }

    // Call this from a Close button
    public void CloseMenu()
    {
        isOpen = false;
        if (taskPanel != null)
            taskPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}