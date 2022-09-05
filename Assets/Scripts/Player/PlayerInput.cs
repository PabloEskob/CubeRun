using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Movement _movement;

    private const string VerticalAxis = "Vertical";

    private void Update()
    {
       _movement.SetDistanceLeg(Input.GetAxis(VerticalAxis));
    }
}
