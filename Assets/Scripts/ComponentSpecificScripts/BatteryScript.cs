using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Battery : CompoScript
{

    public AudioClip boom;
    private AudioSource source;
    public ParticleSystem explosion;

    private void Start()
    {
        source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        isPower = true;
    }
    override public void Activate()
    {
        isActive = true;
    }
    public override void Deactivate()  
    {
        isActive = false;
    }
    public override void Explode()
    {
        explosion.Play();
        source.PlayOneShot(boom, 1);
    }
}
