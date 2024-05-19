using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace HexTecGames
{
	public class MusicPlayer : MonoBehaviour
	{
		[SerializeField] private int transitionTime1 = default;
		[SerializeField] private int transitionTime2 = default;

		[SerializeField] private AudioMixer mixer = default;
		[SerializeField] private float transitionDuration = default;

		private int index = 0;
		private float timer;

        void Update()
        {
			timer += Time.deltaTime;
			if (index == 0 && transitionTime1 <= timer)
			{
				mixer.FindSnapshot("Alert").TransitionTo(transitionDuration);
				index++;
				
			}
			else if (index == 1 && transitionTime2 <= timer)
			{
                mixer.FindSnapshot("Action").TransitionTo(transitionDuration);
            }
        }
    }
}