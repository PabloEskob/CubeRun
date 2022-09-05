using System.Collections;
using DG.Tweening;
using RootMotion.Dynamics;
using RootMotion.FinalIK;
using UnityEngine;

public class Loss : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AnimationPlayer _animationPlayer;
    [SerializeField] private Movement _movement;
    [SerializeField] private PuppetMaster _puppetMaster;
    [SerializeField] private Trail _trailLeft;
    [SerializeField] private Trail _trailRight;
    [SerializeField] private StartGame _startGame;
    [SerializeField] private UI _ui;
    [SerializeField] private PlayUI _playUI;
    [SerializeField] private FullBodyBipedIK _fullBodyBipedIK;
    [SerializeField] private Transform _rightLegPos;
    [SerializeField] private Transform _newRightLeg;
    [SerializeField] private LeftLeg _leftLeg;
    [SerializeField] private RightLeg _rightLeg;
    [SerializeField] private float _duration;
    [SerializeField] private Vector3 _rotate;
    [SerializeField] private float _waitForSecond;
    
    private void OnEnable()
    {
        _player.Lossed += DisableGameScene;
    }

    private void OnDisable()
    {
        _player.Lossed -= DisableGameScene;
    }

    private void Start()
    {
        _puppetMaster.mode = PuppetMaster.Mode.Kinematic;
    }

    private void DisableGameScene()
    {
        DisableAnimatorPlayer();
        _movement.Play(false);
        DisableIK();
        StopTrail();
        _startGame.RaisePriority();
        _ui.LossGame();
        _playUI.TurnOffGame();
    }

    private void DisableAnimatorPlayer()
    {
        _animationPlayer.EndPlaying();
    }

    private void DisableIK()
    {
        _fullBodyBipedIK.transform.DORotate(_rotate, _duration);
        _rightLegPos.transform.parent = null;
        _rightLegPos.transform.DOLocalMove(_newRightLeg.position, _duration);
        _rightLeg.LoseLeg();
        _fullBodyBipedIK.solver.leftFootEffector.target = null;
         StartCoroutine(PupetMasterDead());
    }

    private void StopTrail()
    {
        _trailLeft.enabled = false;
        _trailRight.enabled = false;
    }

    private IEnumerator PupetMasterDead()
    {
        var waitForSecond = new WaitForSeconds(_waitForSecond);
        yield return waitForSecond;
        _puppetMaster.state = PuppetMaster.State.Dead;
        yield return waitForSecond;
        _leftLeg.LoseLeg();
    }
}