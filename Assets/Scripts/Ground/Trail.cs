using UnityEngine;

public class Trail : MonoBehaviour
{
   [SerializeField] private Legs _legs;
   private void Start()
   {
      gameObject.transform.position = _legs.transform.position;
   }

   private void Update()
   {
      gameObject.transform.position = new Vector3(_legs.transform.position.x, 0, _legs.transform.position.z);
   }
}
