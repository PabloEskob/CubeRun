using System.Collections;
using Cinemachine;
using UnityEngine;

public class StartGame : MonoBehaviour
{
   [SerializeField] private Movement _movement;
   [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
   [SerializeField] private float _startTime;
   [SerializeField] private int _priority;

   private IEnumerator _coroutine;

   private void Start()
   {
      _movement.Play(false);
      _coroutine = StartMovement();
      StartCoroutine(_coroutine);
   }
   
   public void RaisePriority()
   {
      _cinemachineVirtualCamera.m_Priority+=_priority;
   }

   private void LowerPriority()
   {
      _cinemachineVirtualCamera.m_Priority-=_priority;
   }
   
   private IEnumerator StartMovement()
   {
      var start = new WaitForSeconds(_startTime);
      yield return start;
      LowerPriority();
      yield return start;
      _movement.Play(true);
   }
}
