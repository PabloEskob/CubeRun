using UnityEngine;

public class CubeRay : MonoBehaviour
{
    [SerializeField] private Legs _legs;
    [SerializeField] private float _value;

    public bool DefineCube()
    {
        Ray ray = new Ray(_legs.transform.position, transform.right * _value);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<BoxCollider>())
            {
                return true;
            }
        }
        return false;
    }
}