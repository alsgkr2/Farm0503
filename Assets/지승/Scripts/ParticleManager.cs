using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public static ParticleManager instance;
    /*
     0. CreateFiled
     1. Harvest
     */
    [SerializeField] ParticleSystem[] particles;
    // Start is called before the first frame update
    private void Start()
    {
        instance=this;
    }
    public void PlayParticle(string mode,Transform pos)
    {

        foreach(ParticleSystem P in particles)
        {
            P.transform.position = pos.position;
        }
        switch (mode)
        {
            case "CreateFiled":
                if (particles[0].isPlaying)
                {
                    particles[0].Stop();
                }
                else
                {
                    particles[0].Play();
                }

               
                break;

            case "Harvest":
                particles[1].Play();
                
                break;

                
        }
    }

}
