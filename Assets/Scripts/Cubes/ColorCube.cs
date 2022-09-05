using System.Collections;
using System.Linq;
using UnityEngine;

public class ColorCube : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Color _targetColor;
    [SerializeField] private float _blinkTime;
    [SerializeField] private Color _blackColor;
    [SerializeField] private Color _colorEnd;
    [SerializeField] private LeftLeg _leftLeg;
    [SerializeField] private RightLeg _rightLeg;
    [SerializeField] private Color[] _colors;
    [SerializeField] private int _countColor;
    [SerializeField] private float _delay;

    private Gradient _gradient;
    private GradientColorKey[] _colorKey;
    private GradientAlphaKey[] _alphaKey;
    private Color _greenColor;
    private Color _intialColor;
    private Color _materialColor;
    private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");

    private void OnEnable()
    {
        _player.ChangedColorWhiteLegCube += BlinkWhiteLeg;
        _player.ChangedColorBlackCube += DeleteCube;
        _player.ChangedColorCube += ChangeColorCube;
    }


    private void OnDisable()
    {
        _player.ChangedColorWhiteLegCube -= BlinkWhiteLeg;
        _player.ChangedColorBlackCube -= DeleteCube;
        _player.ChangedColorCube -= ChangeColorCube;
    }

    private void Start()
    {
        _gradient = new Gradient();
        _colorKey = new GradientColorKey[2];
        _alphaKey = new GradientAlphaKey[2];
        _colors = new Color[_countColor];
        _greenColor = _leftLeg.gameObject.GetComponent<Renderer>().material.color;
        ChangeGradiends(_colorEnd);
    }

    private void BlinkWhiteLeg(Legs legs)
    {
        StartCoroutine(WhiteColor(legs));
    }

    private void DeleteCube(Legs legs)
    {
        legs.Cubes.Last().gameObject.GetComponent<Renderer>().material.color = _blackColor;
    }

    private IEnumerator WhiteColor(Legs legs)
    {
        var color = legs.gameObject.GetComponent<Renderer>().material.color;
        var newWaitForSecond = new WaitForSeconds(_blinkTime);
        legs.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(_targetColor, color, _blinkTime);
        yield return newWaitForSecond;
        legs.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(color, _targetColor, _blinkTime);
    }

    private void ChangeGradiends(Color color)
    {
        _colorKey[0].color = color;
        _colorKey[0].time = 0.0f;
        _colorKey[1].color = _greenColor;
        _colorKey[1].time = 1.0f;
        _alphaKey[0].alpha = 1f;
        _alphaKey[0].time = 0.0f;
        _alphaKey[1].alpha = 1f;
        _alphaKey[1].time = 1.0f;

        _gradient.SetKeys(_colorKey, _alphaKey);

        for (int i = 0; i < _colors.Length; i++)
        {
            float value = i * 100 / _colors.Length;
            _colors[i] = _gradient.Evaluate(value / 100);
        }
    }

    private void ChangeColorCube()
    {
        ChangeGradiends(_colorEnd);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);
        var rightCount = _rightLeg.Cubes.Count;
        var leftLeg = _leftLeg.Cubes.Count;
        Legs moreCube;
        Legs lessCube;

        if (rightCount > leftLeg)
        {
            moreCube = _rightLeg;
            lessCube = _leftLeg;
        }
        else
        {
            moreCube = _leftLeg;
            lessCube = _rightLeg;
        }

        if (moreCube != null && lessCube != null)
        {
            if (moreCube.Cubes.Count > lessCube.Cubes.Count)
            {
                var value = moreCube.Cubes.Count - lessCube.Cubes.Count;
                var color = _colors[_player.ValueLosing - value];
                ChangeGradiends(color);

                for (int i = 0; i < lessCube.Cubes.Count; i++)
                {
                    lessCube.Cubes[i].gameObject.GetComponent<Renderer>().material.color = _colors[i];
                    lessCube.Cubes[i].gameObject.GetComponent<Renderer>().sharedMaterial.SetColor(OutlineColor,_colors[i]);
                }
            }
        }
    }
}