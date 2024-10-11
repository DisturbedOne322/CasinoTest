using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoardNumber : MonoBehaviour
{
    public event Action<int> OnButtonClicked;

    private Button _button;

    [SerializeField, Range(0, 36)]
    private int _number;
    public int Number => _number;

    private int _placedBetsAmount = 0;
    public int PlacedBetsAmount => _placedBetsAmount;

    public void AddBetAmount(int amount) => _placedBetsAmount += amount;

    private void Awake()
    {
        _button = GetComponent<Button>();
        //to make sure the displayed number is correct
        _button.GetComponentInChildren<TextMeshProUGUI>().text = _number.ToString();
        _button.onClick.AddListener(() => OnButtonClicked?.Invoke(_number));
    }
}
