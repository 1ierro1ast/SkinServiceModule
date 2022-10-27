using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.SkinServiceModule
{
    public class SkinProgressView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Image _backImage;
        [SerializeField] private Text _text;
        [SerializeField] private string _suffix;
        [SerializeField] private float _delay;
        [SerializeField] private float _pauseTime;

        private float _newPercent;

        private void Start()
        {
            SetProgress(0);
        }

        public void SetStartProgress(int coinsHas, int coinsNeed = 100)
        {
            float a = coinsNeed;
            float b = coinsHas;
            //Debug.Log("Coins: " + b + "/" + a);
            if (b > 0)
            {
                _image.fillAmount = b/a;
            }
        }

        private void OnEnable()
        {
            StartCoroutine(Progress(_newPercent));
        }

        public void SetProgress(int coinsHas, int coinsNeed = 100)
        {
            float a = coinsNeed;
            float b = coinsHas;
            if (b > 0)
            {
                _newPercent = b/a;
            }
        }

        public void UpdateOutfitView(Sprite background, Sprite foreground)
        {
            _image.sprite = foreground;
            _image.SetNativeSize();
            _backImage.sprite = background;
            _backImage.SetNativeSize();
        }
    
        IEnumerator Progress(float border)
        {
            yield return new WaitForSeconds(_pauseTime);
            var i = _image.fillAmount;
            while (i < border)
            {
                i += 0.05f;
                _image.fillAmount = i;
                if(_text != null) _text.text = (_image.fillAmount * 100).ToString("0") + _suffix;
                yield return new WaitForSeconds(_delay);
                if (_image.fillAmount >= 0.99f)
                {
                    print("new item!");
                
                    yield break;
                }
            }
            yield return null;
        }
    }
}
