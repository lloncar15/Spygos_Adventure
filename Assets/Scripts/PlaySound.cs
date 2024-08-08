using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    [SerializeField] private List<AudioClip> _walk, _attack, _specialAttack1, _specialAttack2, _specialAttack3, _death;

    public AudioClip _earth;


    public void onWalk()
    {
        AudioClip sound = _walk[Random.Range(0, _walk.Count)];
        SoundManager.Instance.PlaySound(sound);
    }

    public void onAttack()
    {
        AudioClip sound = _attack[Random.Range(0, _attack.Count)];
        SoundManager.Instance.PlaySound(sound);
    }

    public void onSpecialAttack1()
    {
        AudioClip sound = _specialAttack1[Random.Range(0, _specialAttack1.Count-1)];
        if(Random.Range(0.0f, 1.0f) < 0.05f)
        {
            sound = _specialAttack1[ _specialAttack1.Count-1];
        }
        SoundManager.Instance.PlaySound(sound);
    }

    public void onSpecialAttack2()
    {
        AudioClip sound = _specialAttack2[Random.Range(0, _specialAttack2.Count)];
        SoundManager.Instance.PlaySound(sound);
    }

    public void onSpecialAttack3()
    {
        AudioClip sound = _specialAttack3[Random.Range(0, _specialAttack3.Count)];
        SoundManager.Instance.PlaySound(sound);
    }

    public void onDeath()
    {
        AudioClip sound = _death[Random.Range(0, _death.Count)];
        SoundManager.Instance.PlaySound(sound);
    }

    public void onEarth()
    {
        AudioClip sound = _earth;
        SoundManager.Instance.PlaySound(sound);
    }
}
