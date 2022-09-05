using UnityEngine;
using RootMotion.FinalIK;
using DG.Tweening;

public class InverseKinematics : MonoBehaviour
{
    [SerializeField] private FullBodyBipedIK _fullBodyBipedIK;
    [SerializeField] private Player _player;
    [SerializeField] private float _height;
    [SerializeField] private float _speedHeight;

    private float _value = 0;

    private void OnEnable()
    {
        _player.Raised += LiftBodyUp;
        _player.Lowered += PutBodyDown;
        _player.BentDown += ChangeBodyAngle;
    }

    private void OnDisable()
    {
        _player.Raised -= LiftBodyUp;
        _player.Lowered -= PutBodyDown;
        _player.BentDown -= ChangeBodyAngle;
    }

    private void LiftBodyUp()
    {
        ChangeBodyHeight(_height);
    }

    private void PutBodyDown()
    {
        ChangeBodyHeight(-_height);
    }

    private void ChangeBodyHeight(float value)
    {
        _fullBodyBipedIK.transform.DOMoveY(_fullBodyBipedIK.transform.position.y + value, _speedHeight);
    }

    private void ChangeBodyAngle(float angle)
    {
        _value += angle;
        _fullBodyBipedIK.transform.DORotate(new Vector3(0, 0, _value), _speedHeight);
    }
    
}