using UnityEngine;

[RequireComponent(typeof(ControlsManager))]
public class Managers : MonoBehaviour
{
    public static ControlsManager Controls { get; private set;}

    private void Awake()
    {
        /// Persist managers between scenes
        DontDestroyOnLoad(gameObject);

        Controls = GetComponent<ControlsManager>();

        Controls.Startup();
    }
}
