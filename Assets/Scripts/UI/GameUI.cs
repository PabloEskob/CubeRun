using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Player _player;
    [SerializeField] private Finish _finish;

    private void Start()
    {
        _slider.minValue = _player.transform.position.z;
        _slider.maxValue = _finish.transform.position.z;
    }

    private void Update()
    {
        _slider.value = _slider.minValue + _player.transform.position.z;
    }
}
