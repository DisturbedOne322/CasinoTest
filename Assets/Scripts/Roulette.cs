using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette : MonoBehaviour
{
    public event Action<int> OnSpinEnd;
    private RouletteSpinner _rouletteSpinner;
    private int _winningNumber = 0;

    private void Start()
    {
        _rouletteSpinner = new(this);
        _rouletteSpinner.OnSpinEnd += _rouletteSpinner_OnSpinEnd;
    }

    private void OnDestroy()
    {
        _rouletteSpinner.OnSpinEnd -= _rouletteSpinner_OnSpinEnd;
    }

    private void _rouletteSpinner_OnSpinEnd()
    {
        OnSpinEnd?.Invoke(_winningNumber);
    }

    public void Spin()
    {
        _winningNumber = UnityEngine.Random.Range(0, 10);
        _rouletteSpinner.SpinToNumber(_winningNumber);
    }
}
