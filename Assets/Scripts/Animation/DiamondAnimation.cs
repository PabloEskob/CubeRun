using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class DiamondAnimation : MonoBehaviour
{
    [SerializeField] private float _endValue;
    [SerializeField] private float _duration;
    [SerializeField] private float _speedRotate;
    [SerializeField] private Vector3 _rotationAngle;
    [SerializeField] private float _durationScale;
    [SerializeField] private PlayUI _playUI;

    private void Start()
    {
        transform.DOLocalMoveY(_endValue, _duration).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        transform.Rotate(_rotationAngle * _speedRotate * Time.deltaTime);
    }

    public void Selection()
    {
        transform.DOScale(Vector3.zero, _durationScale);
        _playUI.CreateDiamond();
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}