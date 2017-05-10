using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;
    public string PathToSounds = "Sounds/";

    private Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
    private AudioSource[] audioSource = new AudioSource[4];
    private AudioSource musicSource;
    private bool[] audioSourceAvailable = new bool[4];


    // Use this for initialization
    void Awake () {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a SoundManager.
            Destroy(gameObject);
    }

    void Start()
    {
        Initialize();
        audioSource[0] = gameObject.AddComponent<AudioSource>();
        audioSource[1] = gameObject.AddComponent<AudioSource>();
        audioSource[2] = gameObject.AddComponent<AudioSource>();
        audioSource[3] = gameObject.AddComponent<AudioSource>();
        audioSourceAvailable[0] = false;
        audioSourceAvailable[1] = false;
        audioSourceAvailable[2] = false;
        audioSourceAvailable[3] = false;
        musicSource = gameObject.AddComponent<AudioSource>();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void Initialize()
    {
        sounds.Add("DeathEnemy", Resources.Load<AudioClip>(PathToSounds + "DeathEnemy"));
        sounds.Add("WrongCombo", Resources.Load<AudioClip>(PathToSounds + "WrongCombo"));
        sounds.Add("RightCombo", Resources.Load<AudioClip>(PathToSounds + "RightCombo"));
        sounds.Add("MusicInGame", Resources.Load<AudioClip>(PathToSounds + "MusicInGame"));
        sounds.Add("MusicBossInGame", Resources.Load<AudioClip>(PathToSounds + "MusicBossInGame"));
        sounds.Add("MusicMenu", Resources.Load<AudioClip>(PathToSounds + "MusicMenu"));
        sounds.Add("Cutscene", Resources.Load<AudioClip>(PathToSounds + "Cutscene"));

        foreach (KeyValuePair<string, AudioClip> sound in sounds)
        {
            if (!sound.Value) {
                Debug.LogError("Invalid sound \"" + sound.Key + "\"");
                //Debug.Log(sound.Value);
            }
               
        }
    }

    public void PlayerMusic(string name)
    {
        if (!sounds.ContainsKey(name))
        {
            Debug.LogError("There is no sound with name \"" + name + "\"");
            return;
        }

        musicSource.clip = sounds[name];
        musicSource.loop = true;
        musicSource.Play();
    }

    void Update()
    {
        for (int i = 0; i < audioSource.Length; i++)
            if (!audioSource[i].isPlaying)
                audioSourceAvailable[i] = false;
    }

    private int findAvailable()
    {
        for (int i = 0; i < audioSourceAvailable.Length; i++)
            if (!audioSourceAvailable[i])
                return i;
        return 0;
    }

    public void Play(string name, bool loop)
    {
        if (!sounds.ContainsKey(name))
        {
            Debug.LogError("There is no sound with name \"" + name + "\"");
            return;
        }
        int index = findAvailable();
        audioSource[index].clip = sounds[name];
        audioSource[index].loop = loop;
        audioSource[index].Play();
        audioSourceAvailable[index] = true;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void Stop()
    {
        for (int i = 0; i < audioSourceAvailable.Length; i++)
        audioSource[i].Stop();
    }
}
