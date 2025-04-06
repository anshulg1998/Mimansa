using System;
using System.IO;
using HuggingFace.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class SpeechAPI
{ 
	private static  byte[] bytes;

	public static void  CallAPI(AudioClip clip, Action<string> callback)
	{
		var position = Microphone.GetPosition(null);
		Microphone.End(null);
		var samples = new float[position * clip.channels];
		clip.GetData(samples, 0);
		bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
		SendRecording(callback);
	}

	private static void SendRecording(Action<string> callback)
	{
		string str = "";
		HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
			str = response;
			callback?.Invoke(str);
		}, error => {
			
		});
	}

	private static byte[] EncodeAsWAV(float[] samples, int frequency, int channels)
	{
		using (var memoryStream = new MemoryStream(44 + samples.Length * 2))
		{
			using (var writer = new BinaryWriter(memoryStream))
			{
				writer.Write("RIFF".ToCharArray());
				writer.Write(36 + samples.Length * 2);
				writer.Write("WAVE".ToCharArray());
				writer.Write("fmt ".ToCharArray());
				writer.Write(16);
				writer.Write((ushort)1);
				writer.Write((ushort)channels);
				writer.Write(frequency);
				writer.Write(frequency * channels * 2);
				writer.Write((ushort)(channels * 2));
				writer.Write((ushort)16);
				writer.Write("data".ToCharArray());
				writer.Write(samples.Length * 2);

				foreach (var sample in samples)
				{
					writer.Write((short)(sample * short.MaxValue));
				}
			}
			return memoryStream.ToArray();
		}
	}
}