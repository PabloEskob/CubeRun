using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(Cube))]
[RequireComponent(typeof(Rigidbody))]
public class Legs : MonoBehaviour
{
    [SerializeField] private float _speedLeg;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private Cube _prefabCube;
    [SerializeField] private List<Cube> _cubes;
    [SerializeField] private float _timeFootDown;
    [SerializeField] private Ground _ground;
    [SerializeField] private float _minGround;
    [SerializeField] private CubeRay _cubeRay;
    [SerializeField] private float _durationChangeLegs;
    [SerializeField] private PhysicMaterial _physicMaterial;

    private Cube _cube;
    private Vector3 _intialTransformPosition;
    private float _liftDistance;

    public List<Cube> Cubes => _cubes;
    public float MinDistance => _minDistance;

    public event UnityAction<Legs, Cube> Faced;
    public event UnityAction<Legs> ChangedCountCubes;

    private void Awake()
    {
        _cubes.Add(gameObject.GetComponent<Cube>());
        _liftDistance = _prefabCube.transform.localScale.y * 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        _cube = other.GetComponent<Cube>();

        if (_cube != null)
        {
            Faced?.Invoke(this, _cube);
        }
    }

    public void MoveLegs(float getAxis)
    {
        float offset = getAxis * _speedLeg * Time.deltaTime;
        offset = Mathf.Clamp(offset, _minDistance - transform.localPosition.x,
            _maxDistance - transform.localPosition.x);
        transform.Translate(Vector3.left * offset, Space.World);
    }

    public void AddCube()
    {
        RaiseLeg();
        var newCube = Instantiate(_prefabCube,
            new Vector3(transform.position.x, _cubes.Last().transform.position.y - _liftDistance, transform.position.z),
            Quaternion.identity);
        newCube.transform.parent = gameObject.transform;
        newCube.gameObject.AddComponent<BoxCollider>().material=_physicMaterial;
        _cubes.Add(newCube);
        ChangedCountCubes?.Invoke(this);
    }

    public void DeleteCube()
    {
        var cube = gameObject.transform.GetChild(gameObject.transform.childCount - 1);
        cube.transform.parent = null;
        _cubes.RemoveAt(_cubes.LastIndexOf(_cubes.Last()));
        ChangedCountCubes?.Invoke(this);
        StartCoroutine(PutFootDown());
    }

    public bool DefineTower()
    {
        return _cubeRay.DefineCube();
    }

    public void DisplayText(TMP_Text tmpText)
    {
        tmpText.text = _cubes.Count.ToString();
    }

    public void LoseLeg()
    {
        var rigidBody = gameObject.GetComponent<Rigidbody>();

        foreach (var cube in _cubes)
        {
            if (!cube.gameObject.GetComponent<Rigidbody>())
            {
                cube.gameObject.AddComponent<Rigidbody>();
            }
        }
        rigidBody.isKinematic = false;
        rigidBody.useGravity = true;
    }

    private void RaiseLeg()
    {
        _intialTransformPosition = transform.position;
        _intialTransformPosition.y += _liftDistance;
        transform.DOMoveY(_intialTransformPosition.y, _durationChangeLegs);
    }

    private IEnumerator PutFootDown()
    {
        var waitForSecond = new WaitForSeconds(_timeFootDown);
        yield return waitForSecond;
        transform.DOMoveY(_ground.gameObject.transform.position.y + _liftDistance * _cubes.Count - _minGround,
            _durationChangeLegs);
    }
}