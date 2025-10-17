using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"There should only be 1 {name} in the scene");
            gameObject.SetActive(false);
        }
    }

    public delegate void ClearAllParentsEvent();
    public static ClearAllParentsEvent clearAllParentsEvent;    // this is to make an instance of the previous delegate, and there'll be only of it
}
