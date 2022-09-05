using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Menu : MonoBehaviour
{
   [SerializeField] private MenuNoActive _menuNoActive;
   [SerializeField] private float _timeForOff;
   [SerializeField] private Image[] _images;
   [SerializeField] private TMP_Text[] _tmpText;
   [SerializeField] private Color _color;
   [SerializeField] private Color _colorEnd;

   private IEnumerator _coroutine;

   private void Start()
   {
      _coroutine = TurnOff();
      StartCoroutine(_coroutine);
   }

   private IEnumerator TurnOff()
   {
      DisableImage(_timeForOff,_color);
      _menuNoActive.gameObject.SetActive(true);
      var waitForSeconds = new WaitForSeconds(_timeForOff);
      yield return waitForSeconds;
      DisableImage(_timeForOff,_colorEnd);
      _menuNoActive.gameObject.SetActive(false);
   }

   private void DisableImage(float timeDisable,Color color)
   {
      foreach (var image in _images)
      {
         image.DOColor(color, timeDisable);
      }

      foreach (var text in _tmpText)
      {
         text.DOColor(color, timeDisable);
      }
   }
}
