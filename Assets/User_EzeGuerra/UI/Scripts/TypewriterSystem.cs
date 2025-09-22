using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterSystem : MonoBehaviour
{
    private int _currentCharIndex;
    private Coroutine _typewriterCoroutine;
    private WaitForSeconds _delay;

    [Header("Ajustes")]
    [SerializeField] private float _charsPerSecond = 20;
    private TMP_Text _textBox;

    private void Awake()
    {
        _textBox = GetComponent<TMP_Text>();
        _delay = new WaitForSeconds(1 / _charsPerSecond);
    }

    private void OnEnable() //llamar corrutina cuando se activa el objeto
    {
        SetText(); 
    }

    public void SetText()
    {
        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);

        _textBox.maxVisibleCharacters = 0; //este sistema no suma los caracteres de a 1, sino que los "desoculta"
        _currentCharIndex = 0;
        
        _typewriterCoroutine = StartCoroutine(Typewriter());
    }

    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = _textBox.textInfo;

        while (_currentCharIndex < textInfo.characterCount + 1)
        {
            var lastCharIndex = textInfo.characterCount - 1;

            if (_currentCharIndex == lastCharIndex) //si destapa todos los caracteres, cortar corrutina
            {
                _textBox.maxVisibleCharacters++;
                yield break;
            }

            _textBox.maxVisibleCharacters++;//sino, seguir escribiendo
            yield return _delay;

            _currentCharIndex++;
        }
    }
}
