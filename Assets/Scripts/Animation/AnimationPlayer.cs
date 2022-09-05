using System.Collections;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _changeAnimTime;

    private bool _play = true;
    private bool _value;
    private static readonly int Swing = Animator.StringToHash("Swing");
    private IEnumerator _coroutine;

    private void Start()
    {
        _coroutine = ChangeAnimation();
        StartCoroutine(_coroutine);
    }

    public void EndPlaying()
    {
        _animator.enabled = false;
    }

    private IEnumerator ChangeAnimation()
    {
        while (_play)
        {
            _value = !_value;
            var waitForSeconds = new WaitForSeconds(_changeAnimTime);
            yield return waitForSeconds;
            _animator.SetBool(Swing, _value);
        }
    }
}