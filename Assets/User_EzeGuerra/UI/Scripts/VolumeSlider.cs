using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
	[SerializeField] private AudioMixer _audioMixer;
	[SerializeField] private string _parameterName;
	
	private void Start()
    {
		float volGuardado = PlayerPrefs.GetFloat(_parameterName, _slider.maxValue);
		SetearVolumen(volGuardado); 
        
		_slider.value = volGuardado;
		
		//las clases de UI (como slider) usan eventos, requiriendo delegados (delegate(float _) {SetearVolumen(_);}) 
		//o expresiones lambda ((float _) => SetearVolumen(_))
		_slider.onValueChanged.AddListener((float _) => SetearVolumen(_)); }
	
	private void SetearVolumen(float valorVol)
	{
		//dividir por el valor max permite ajustar al valor máximo actual del slider
		_audioMixer.SetFloat(_parameterName, ConvertirADecibel(valorVol/_slider.maxValue));
		
		PlayerPrefs.SetFloat(_parameterName, valorVol); //guardar valor entre escenas
	}

	//convierte el porcentaje-fraccion a decibeles,
	//con un clamp minimo de 0.0001 y un minimo de -80dB, como los mixers de Unity
	public float ConvertirADecibel(float valor)
	{
		return Mathf.Log10(Mathf.Max(valor, 0.0001f)) * 20f;
	}
}
