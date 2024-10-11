using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette : MonoBehaviour
{
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
        PublishResults();
    }

    private void PublishResults()
    {

    }

    public void Spin()
    {
        _winningNumber = UnityEngine.Random.Range(0, 10);
        _rouletteSpinner.SpinToNumber(_winningNumber);
    }
}
