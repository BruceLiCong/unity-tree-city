using UnityEngine;
using UnityEngine.UI;

public class SettingsModal : MonoBehaviour
{
    [SerializeField]
    private Slider _sensitivitySlider;

    [SerializeField]
    private Text _sensitivityLabel;

    [SerializeField]
    private Slider _moveSpeedSlider;

    [SerializeField]
    private Text _moveSpeedLabel;

    private void Start()
    {
        _sensitivitySlider.value = Managers.Controls.MouseSensitivity;
        _sensitivityLabel.text = Managers.Controls.MouseSensitivity.ToString();

        _moveSpeedSlider.value = Managers.Controls.MoveSpeed;
        _moveSpeedLabel.text = Managers.Controls.MoveSpeed.ToString();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnMoveSpeedValue(float value)
    {
        Managers.Controls.UpdateMoveSpeed(value);
        _moveSpeedLabel.text = value.ToString();
    }

    public void OnMouseSensitivityValue(float value)
    {
        Managers.Controls.UpdateSensitivity(value);
        _sensitivityLabel.text = value.ToString();
    }
}
