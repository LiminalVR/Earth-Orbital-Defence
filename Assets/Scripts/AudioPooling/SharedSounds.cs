using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedSounds : MonoBehaviour
{
    public static SharedSounds Instance;
    public List<AudioClip> LaserSounds;
    public AudioClip LaserStop;
    public List<AudioClip> Explosions;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
}
