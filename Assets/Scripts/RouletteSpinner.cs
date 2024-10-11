using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class RouletteSpinner
{
    public event Action OnSpinEnd;
    private Roulette _roulette;

    private const float FULL_SPIN_ANGLE = 360;
    private const float SPIN_DURATION = 3f;

    public RouletteSpinner(Roulette roulette)
    {
        _roulette = roulette;
    }

    public void SpinToNumber(int numberToSpinTo)
    {
        float targetAngle = NumberToAngleMatcher.GetAngleForNumber(numberToSpinTo) + FULL_SPIN_ANGLE * UnityEngine.Random.Range(2, 5);
        _roulette.transform.DORotate(new Vector3(0, 0, targetAngle), SPIN_DURATION, RotateMode.FastBeyond360).OnComplete(() => NotifyOnSpinEnd());
    }

    private void NotifyOnSpinEnd() => OnSpinEnd?.Invoke();

    //If I need to rotate only to specific number from 0-9 on the 36 number roulette, I have to know what angle to rotate to
    //since the angle corresponding to the number doesn't change, keep them constant
    private static class NumberToAngleMatcher
    {
        private const float ZERO = 0f;
        private const float ONE = 223.79f;
        private const float TWO = 58.38f;
        private const float THREE = 340.55f;
        private const float FOUR = 38.92f;
        private const float FIVE = 184.87f;
        private const float SIX = 97.3f;
        private const float SEVEN = 301.63f;
        private const float EIGHT = 155.72f;
        private const float NINE = 262.81f;

        public static float GetAngleForNumber(int number)
        {
            return number switch
            {
                0 => ZERO,
                1 => ONE,
                2 => TWO,
                3 => THREE,
                4 => FOUR,
                5 => FIVE,
                6 => SIX,
                7 => SEVEN,
                8 => EIGHT,
                9 => NINE,
                _ => 0,
            };
        }
    }
}
