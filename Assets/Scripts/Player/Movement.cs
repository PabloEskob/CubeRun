using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speedForward;
    [SerializeField] private LeftLeg _leftLeg;
    [SerializeField] private RightLeg _rightLeg;
    
    private float _moveLegs;
    private bool _isPlaying=true;
    
    private void Update()
    {
        if (_isPlaying)
        {
            MoveForward();
            ChangeLegStretch();
        }
    }

    public void Play(bool playing)
    {
        _isPlaying = playing;
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.back * _speedForward * Time.deltaTime);
    }

    public void SetDistanceLeg(float getAxis)
    {
        _moveLegs = getAxis;
    }

    private void ChangeLegStretch()
    {
        _leftLeg.MoveLegs(_moveLegs);
        _rightLeg.MoveLegs(-_moveLegs);
    }
}