using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private SettingsModal _settingsModal;

    private void Start()
    {
        _settingsModal.Hide();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            OnOpenSettings();
        }
    }

    public void OnOpenSettings()
    {
        _settingsModal.Show();
    }
}
