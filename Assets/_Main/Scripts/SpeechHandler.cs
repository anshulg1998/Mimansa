using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechHandler : MonoBehaviour, IResettable
{
    // Start is called before the first frame update
    void Start()
    {
		ServiceLocator.Get<RaceManager>().SubscribeResettableObjects((IResettable)this);
	}

	bool recording  = false;
	bool speechRegistered = false;
	AudioClip clip;
	private Action<string> callback;
	public void Initialise(Action<string> Onvoicecallback)
	{
		this.callback = Onvoicecallback;
		StartRecording();
	}

	void Update()
	{
		if (!recording) return;

		float volume = GetMicLoudness();

		if (volume > 0.1f) // Adjust threshold as needed
		{
			speechRegistered = true;
		}

		else if( volume < .1f && speechRegistered )
		{
			StopRecording(clip);
			StartRecording();
		}
	}


	private void StartRecording()
	{
		recording = true;
		clip = Microphone.Start(null, false, 10, 44100);
	}

	public void StopRecording(AudioClip clip)
	{
		recording = false;
		speechRegistered = false;
		SpeechAPI.CallAPI(clip, ReceiveText);

		
	}

	public void ReceiveText(string text)
	{
		if(text.Length > 0 )
		{
			callback(text);
		}
	}

	float GetMicLoudness()
	{
		int sampleWindow = 128;
		float[] samples = new float[sampleWindow];

		int micPosition = Microphone.GetPosition(Microphone.devices[0]) - (sampleWindow + 1);
		if (micPosition < 0)
			return 0;

		clip.GetData(samples, micPosition);
		float levelMax = 0;

		for (int i = 0; i < sampleWindow; ++i)
		{
			float wavePeak = samples[i];
			if (levelMax < wavePeak)
				levelMax = wavePeak;
		}

		return levelMax;
	}

	public void Reset()
	{
		recording = false;
		speechRegistered = false;
		clip = null;
	}
}
