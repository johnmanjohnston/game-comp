using UnityEngine;
using System.Collections;

// TO DO: TRY USING THIS WITH MULTIPLE DIFFERENT SOUNDTRACKS
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundtracks;
    [SerializeField] private AudioSource soundtrackPlayer;

    private void Start() {
        StartCoroutine(PlayRandomSong());
    }

    private IEnumerator PlayRandomSong() {
        yield return new WaitForSeconds(Random.Range(5, 15));
        soundtrackPlayer.clip = soundtracks[Random.Range(0, soundtracks.Length)];
        soundtrackPlayer.Play();

        yield return new WaitForSeconds(soundtrackPlayer.clip.length);
        StartCoroutine(PlayRandomSong());
    }
}