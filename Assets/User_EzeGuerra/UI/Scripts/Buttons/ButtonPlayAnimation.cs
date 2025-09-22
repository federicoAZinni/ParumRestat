using UnityEngine;

public class ButtonPlayAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationName;
    [SerializeField] private float _delay;

    public void PlayAnimation()
    {
        Invoke("DelayPlayAnimation", _delay);
    }

    private void DelayPlayAnimation()
    {
        _animator.Play(_animationName);
    }
}
