using UnityEngine;
using RootMotion.FinalIK;

public class Hand : MonoBehaviour
{
   [SerializeField] private Legs _legs;
   [SerializeField] private FullBodyBipedIK _fullBodyBipedIK;
   
   private void Update()
   {
      SetValue();
   }

   private void SetValue()
   {
      _fullBodyBipedIK.solver.leftHandEffector.positionWeight = _legs.transform.localPosition.x-_legs.MinDistance;
      _fullBodyBipedIK.solver.rightHandEffector.positionWeight = _legs.transform.localPosition.x-_legs.MinDistance;
   }
}
