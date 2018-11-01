using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutTimer : MonoBehaviour
{
    [SerializeField] private float _startTime = 4f;
    [SerializeField]
    private float _percentageOfPrevTime = 0.99f;
    private float _timeScale = 1f;

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
        _timeLeft -= Time.deltaTime * _timeScale;
        _slider.value = _timeLeft / _startTime;

        if (_timeLeft <= 0f)
        {
            LoseGame();
        }
    }

    public void Pause()
    {
        _timeScale = 0f;
    }

    public void Unpause()
    {
        _timeScale = 1f;
    }

    public void LoseGame()
    {
        print("LOST GAME");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Reset()
    {
        _startTime *= _percentageOfPrevTime;
        _timeLeft = _startTime;
        print(_timeLeft);
    }
}