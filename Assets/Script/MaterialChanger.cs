using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MaterialChanger : MonoBehaviour
{

    public AudioClip tickSound;
    private AudioSource audioSource;

    public Material selectedButtonMaterial;
    [Tooltip("The Original Button material usually")]
    public Material[] buttonInactiveMaterial;
    private Renderer rendrer;

    //public BallMaterials ballMaterials;

    [Header("Ball related")]
    public Material[] redBallMaterials;
    public Material[] yellowBallMaterials;
    public Material[] flashBallMaterials;

    public AudioSource AudioSource
    {
        get      
        {
            if(audioSource == null) audioSource = this.gameObject.GetComponent<AudioSource>();
            return audioSource;
        }

        set
        {
            audioSource = value;
        }
    }

    private void Awake()
    {

        audioSource = this.gameObject.GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = this.gameObject.AddComponent<AudioSource>();

        }
        if (tickSound == null)
        {
            tickSound = Resources.Load<AudioClip>("tick");
        }
        audioSource.clip = tickSound;
        audioSource.Stop();

        /*if (rendrer == null)*/ rendrer = this.GetComponent<Renderer>();
        buttonInactiveMaterial = rendrer.materials;

        //if(ballMaterials != null)
        //{
        //    redBallMaterials = ballMaterials.GetRedBalls();
        //    yellowBallMaterials = ballMaterials.GetYellowBalls();
        //    flashBallMaterials = ballMaterials.GetFlashBalls();
        //}

    }

    public void SetOnMaterial()
    {
        //if (AudioSource != null)
        //{
        //    audioSource.pitch = 1;
        //    audioSource.volume = 1;
        //    audioSource.Stop();
        //    audioSource.Play();
        //}

        rendrer.material = selectedButtonMaterial;
    }

    public void SetMaterial(Material mat)
    {
        //audioSource.pitch = 1;
        //audioSource.volume = 1;

        //audioSource.Stop();
        //audioSource.Play();

        rendrer.material = mat;
    }


    public void SetOffMaterial()
    {
        //if(AudioSource != null)
        //{
        //    audioSource.pitch = 1;
        //    audioSource.Stop();
        //    audioSource.Play();
        //}
        rendrer.materials = buttonInactiveMaterial;
    }

    public void SetFlashMaterial()
    {
        //if(AudioSource != null)
        //{
        //    audioSource.pitch = 1;
        //    audioSource.Stop();
        //    audioSource.Play();
        //}

        rendrer.materials = flashBallMaterials;
    }

    public void SetYellowMaterial()
    {
        //if(AudioSource != null)
        //{
        //    audioSource.pitch = 1;
        //    audioSource.Stop();
        //    audioSource.Play();
        //}
        rendrer.materials = yellowBallMaterials;
    }

    public void SetRedMaterial()
    {
        //if(AudioSource != null)
        //{
        //    audioSource.pitch = 1;
        //    audioSource.Stop();
        //    audioSource.Play();
        //}


        rendrer.materials = redBallMaterials;
    }
    public void SetMaterial(ButtonType type = ButtonType.Green)
    {

        if(type == ButtonType.Red)
        {
            SetRedMaterial();
        }
        else
        {
            SetYellowMaterial();
        }
    }
}
