using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private RightLeg _rightLeg;
    [SerializeField] private LeftLeg _leftLeg;
    [SerializeField] private float _angle;
    [SerializeField] private int _valueLosing;

    private float _tilt;

    public int ValueLosing => _valueLosing;

    public event UnityAction Raised;
    public event UnityAction Lowered;
    public event UnityAction<float> BentDown;
    public event UnityAction Lossed;
    public event UnityAction<Legs> ChangedColorWhiteLegCube;
    public event UnityAction<Legs> ChangedColorBlackCube;
    public event UnityAction ChangedColorCube;

    private void OnEnable()
    {
        _rightLeg.Faced += ChangeLeg;
        _leftLeg.Faced += ChangeLeg;
    }

    private void OnDisable()
    {
        _rightLeg.Faced -= ChangeLeg;
        _leftLeg.Faced -= ChangeLeg;
    }

    private void ChangeLeg(Legs leg, Cube cube)
    {
        if (cube.GetComponent<GreenCube>())
        {
            ChangedColorWhiteLegCube?.Invoke(leg);
            leg.AddCube();
            Destroy(cube.gameObject);
            ChangedColorCube?.Invoke();

            if (KeepPlaying())
            {
                _tilt = Tilt(_angle, leg);
                BentDown?.Invoke(_tilt);

                if (AddAndRemoveCube(leg))
                {
                    Raised?.Invoke();
                }
            }
            else
            {
                Lossed?.Invoke();
            }
        }
        else if (cube.GetComponent<RedCube>())
        {
            ChangedColorBlackCube?.Invoke(leg);
            leg.DeleteCube();
            _tilt = Tilt(-_angle, leg);
            BentDown?.Invoke(_tilt);

            if (AddAndRemoveCube(leg))
            {
                Lowered?.Invoke();
            }
        }
    }

    private bool AddAndRemoveCube(Legs legs)
    {
        bool isTrigger = legs.DefineTower();
        return isTrigger;
    }

    private float Tilt(float angle, Legs leg)
    {
        if (leg == _leftLeg)
        {
            return -angle;
        }
        return angle;
    }

    private bool KeepPlaying()
    {
        var value = _rightLeg.Cubes.Count - _leftLeg.Cubes.Count;

        if (value == _valueLosing || value == -_valueLosing)
        {
            return false;
        }
        return true;
    }
}