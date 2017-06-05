using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static AudioClip respawn;
    public static AudioClip boost;
    public static AudioClip gravityUP;
    public static AudioClip gravityDOWN;
    public static AudioClip[] sounds = new AudioClip[5];
    public static AudioSource[] audioSource = new AudioSource[2];

    public static void StartSounds()
    {
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    sounds[i] = Resources.Load("Sounds/Respawn", typeof(AudioClip)) as AudioClip;
                    break;
                case 1:
                    sounds[i] = Resources.Load("Sounds/Speed", typeof(AudioClip)) as AudioClip;
                    break;
                case 2:
                    sounds[i] = Resources.Load("Sounds/warpUp", typeof(AudioClip)) as AudioClip;
                    break;
                case 3:
                    sounds[i] = Resources.Load("Sounds/warpOut", typeof(AudioClip)) as AudioClip;
                    break;
            }
        }
        for (int i = 0; i < 2; i++)
        {
            audioSource[i] = GameObject.Find("SoundPlayer" + (i + 1)).GetComponent<AudioSource>();
        }
    }

    public static void PlaySound(int currentPlayer, string soundType)
    {
        switch (soundType)
        {
            case "Respawn":
                audioSource[currentPlayer].Stop();
                audioSource[currentPlayer].PlayOneShot(sounds[0]);
                break;
            case "Boost":
                audioSource[currentPlayer].Stop();
                audioSource[currentPlayer].PlayOneShot(sounds[1]);
                break;
            case "GravityUP":
                audioSource[currentPlayer].Stop();
                audioSource[currentPlayer].PlayOneShot(sounds[2]);
                break;
            case "GravityDOWN":
                audioSource[currentPlayer].Stop();
                audioSource[currentPlayer].PlayOneShot(sounds[2]);
                break;
        }
    }
}
