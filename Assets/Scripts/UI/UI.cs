using System;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private RightLeg _rightLeg;
    [SerializeField] private LeftLeg _leftLeg;
    [SerializeField] private TMP_Text _textRightLeg;
    [SerializeField] private TMP_Text _textLeftLeg;

    private void OnEnable()
    {
        _rightLeg.ChangedCountCubes += DisplayCountCubes;
        _leftLeg.ChangedCountCubes += DisplayCountCubes;
    }

    private void OnDisable()
    {
        _rightLeg.ChangedCountCubes -= DisplayCountCubes;
        _leftLeg.ChangedCountCubes += DisplayCountCubes;
    }

    private void Start()
    {
        _textRightLeg.text = _rightLeg.Cubes.Count.ToString();
        _textLeftLeg.text = _leftLeg.Cubes.Count.ToString();
    }

    public void LossGame()
    {
        _textRightLeg.text = String.Empty;
        _textLeftLeg.text = String.Empty;
    }

    private void DisplayCountCubes(Legs legs)
    {
        legs.DisplayText(legs.GetComponentInChildren<TMP_Text>());
    }
}