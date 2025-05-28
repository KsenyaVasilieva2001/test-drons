using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Controls")]
    public Slider speedRedSlider;
    public TMP_Text speedRedText;
    public Slider speedBlueSlider;
    public TMP_Text speedBlueText;

    public TMP_InputField spawnIntervalInput;
    public Button spawnButton;

    public Toggle drawPathToggle;

    public TextMeshProUGUI collectedCountRedText;
    public TextMeshProUGUI collectedCountBlueText;

    public Slider droneCountRedSlider;
    public Slider droneCountBlueSlider;
    public TMP_Text droneCountRedText;
    public TMP_Text droneCountBlueText;

    [SerializeField] private StationManager redStationManager;
    [SerializeField] private StationManager blueStationManager;
    [SerializeField] private Spawner spawner;

    private void Awake()
    {
        speedRedSlider.onValueChanged.AddListener(OnRedSpeedChanged);
        speedBlueSlider.onValueChanged.AddListener(OnBlueSpeedChanged);
        droneCountRedSlider.onValueChanged.AddListener(OnCountRedChanged);
        droneCountBlueSlider.onValueChanged.AddListener(OnCountBlueChanged);
        spawnButton.onClick.AddListener(OnSpawnButtonClik);
        drawPathToggle.onValueChanged.AddListener(OnDrawPathToggleChanged);
    }

    private void Start()
    {
        speedRedText.text = "Скорость: " + speedRedSlider.minValue;
        speedBlueText.text = "Скорость: " + speedBlueSlider.minValue;
        droneCountBlueText.text = "Количество: " + droneCountBlueSlider.minValue;
        droneCountRedText.text = "Количество: " + droneCountRedSlider.minValue;
    }

    private void OnRedSpeedChanged(float value)
    {
        speedRedText.text = "Скорость: " + value.ToString();
        redStationManager.SetSpeed(value);
    }

    private void OnBlueSpeedChanged(float value)
    {
        speedBlueText.text = "Скорость: " + value.ToString();
        blueStationManager.SetSpeed(value);
    }

    public void OnCountBlueChanged(float value)
    {
        int count = Mathf.RoundToInt(value);
        droneCountBlueText.text = "Количество: " + count.ToString();
        blueStationManager.ChangeDroneCount(count);
    } 

    public void OnCountRedChanged(float value)
    {
        int count = Mathf.RoundToInt(value);
        droneCountRedText.text = "Количество: " + count.ToString();
        redStationManager.ChangeDroneCount(count);
    }

    public void OnSpawnButtonClik()
    {
        float num = 0f;
        if (float.TryParse(spawnIntervalInput.text, out num))
        {
            spawner.SetSpawnInterval(num);
        }
    }

    private void OnDrawPathToggleChanged(bool isOn)
    {
        PathVisualizer.Instance.ShowPaths = isOn;
    }
}
