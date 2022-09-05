using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayUI : MonoBehaviour
{
    [SerializeField] private float _timeTurnOff;
    [SerializeField] private Image[] _images;
    [SerializeField] private Color _endColor;
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private float _waitingTimeText;
    [SerializeField] private RestartUI _restartUI;
    [SerializeField] private Color _color;
    [SerializeField] private float _waitingTime;
    [SerializeField] private GameObject _image;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedTo;
    [SerializeField] private Image _finishDiamond;
    [SerializeField] private Image _startPos;
    [SerializeField] private GameObject _finishDiamonds;
    [SerializeField] private Vector3 _scaleFinishDiamond;
    [SerializeField] private float _duration;
    [SerializeField] private TMP_Text _tmpTextDiamond;

    private IEnumerator _coroutineEnd;
    private IEnumerator _startCoroutine;
    private Sequence _sequence;
    private int _score = 0;

    private void Start()
    {
        _startCoroutine = TurnOff(0, 1, _waitingTime, _timeTurnOff, _color, false);
        _coroutineEnd = TurnOff(1, 0, _waitingTime, _timeTurnOff, _endColor, true);
        TurnOn();
    }

    public void CreateDiamond()
    {
        _sequence = DOTween.Sequence();
        var intialFinish = _finishDiamonds.transform;
        var diamond = Instantiate(_image, _startPos.gameObject.transform);
        _sequence.Append(diamond.gameObject.transform.DOMove(_finishDiamond.transform.position,
            Random.Range(_speed, _speedTo)));
        _sequence.Append(diamond.gameObject.transform.DOScale(Vector3.zero, 0f));
        _sequence.Append(_finishDiamonds.transform.DOScale(_scaleFinishDiamond, _duration));
        _sequence.Append(_finishDiamonds.transform.DOScale(intialFinish.localScale, _duration));
        AddScore();
    }

    public void TurnOffGame()
    {
        StartCoroutine(_coroutineEnd);
    }

    private void TurnOn()
    {
        StartCoroutine(_startCoroutine);
    }

    private IEnumerator TurnOff(float a, float b, float time, float waitingTime, Color color, bool turnOn)
    {
        var waitForSecond = new WaitForSeconds(waitingTime);
        yield return waitForSecond;
        var counter = 0f;

        _tmpText.DOColor(color, _waitingTimeText);

        while (counter < time)
        {
            counter += Time.deltaTime;

            foreach (var image in _images)
            {
                var imageColor = image.color;
                imageColor.a = Mathf.Lerp(a, b, counter / time);
                image.color = imageColor;
                yield return null;
            }
        }

        if (turnOn)
        {
            ResetMenu();
        }
    }

    private void ResetMenu()
    {
        _restartUI.TurnOn();
    }

    private void AddScore()
    {
        _score++;
        _tmpTextDiamond.text = _score.ToString();
    }
}