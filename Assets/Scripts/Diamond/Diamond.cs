using UnityEngine;

public class Diamond : MonoBehaviour
{
   [SerializeField] private DiamondAnimation _diamondAnimation;
   
   private void OnTriggerEnter(Collider other)
   {
       if (!other.GetComponent<Ground>())
       {
           _diamondAnimation.Selection();
       }
   }
}
