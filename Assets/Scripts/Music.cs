using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance;

    private void Awake()
    {
        if (instance is null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(instance);
    }
}
