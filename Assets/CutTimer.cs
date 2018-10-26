using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutTimer : MonoBehaviour
{
    [SerializeField] private float _startTime = 4f;
    [SerializeField]
    [Range(0, 1)]
    private float _percentageOfPrevTime = 0.99f;

    private float _timeLeft;
    private Slider _slider;

    private void Start()
    {
        _timeLeft = _startTime;

        _slider = GetComponent<Slider>();
        _slider.value = 1f;
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;
        _slider.value = _timeLeft / _startTime;
    }

    public void Reset()
    {
        _timeLeft = _startTime;
    }
}
