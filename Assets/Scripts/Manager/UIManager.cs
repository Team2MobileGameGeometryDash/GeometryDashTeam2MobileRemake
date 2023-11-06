using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider sliderProgress;
    public TextMeshProUGUI attemptCount;
    public Image WinPanel;

    public void SetSliderValue(float value)
    {
        sliderProgress.value = value;
    }

    public void UpdateAttempt(int value)
    {
        attemptCount.text = value.ToString();
    }

    public void ActiveWinPanel()
    {
        WinPanel.enabled = true;
    }
}
