using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class RestartUI : MonoBehaviour
{
    [SerializeField] private Image _imagePanel;
    [SerializeField] private Color _colorPanel;
    [SerializeField] private float _timePanel;
    [SerializeField] private TryAgain _tryAgain;
    [SerializeField] private Color _colorTMP;
    [SerializeField] private float _durationTmproText;
    [SerializeField] private Retry _retry;
    [SerializeField] private Vector3 _scaleRetry;
    [SerializeField] private Vector3 _lowScaleRetry;
    [SerializeField] private float _durationLowScale;
    [SerializeField] private float _durationRetryScale;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _sliderTime;

    private Sequence _sequence;

    public void TurnOn()
    {
        _imagePanel.DOColor(_colorPanel, _timePanel);
        _tryAgain.gameObject.SetActive(true);
        _tryAgain.GetComponentInChildren<TMP_Text>().DOColor(_colorTMP, _durationTmproText);
        _retry.transform.DOScale(_scaleRetry, _durationRetryScale);
        _slider.DOValue(1, _sliderTime);
        StartCoroutine(RetryAnimation());
    }

    private IEnumerator RetryAnimation()
    {
        var waitForSecond = new WaitForSeconds(_durationRetryScale);
        yield return waitForSecond;
        _sequence = DOTween.Sequence();
        _sequence.Append(_retry.transform.DOScale(_lowScaleRetry, _durationLowScale));
        _sequence.Append(_retry.transform.DOScale(_scaleRetry, _durationRetryScale));
        _sequence.SetLoops(-1, LoopType.Yoyo);
    }
}