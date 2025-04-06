using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMap : MonoBehaviour, IResettable
{
    [SerializeField] List<ParallaxScroll> scrollItems;

	public void Reset()
	{
        foreach (var item in scrollItems)
        {
            item.Reset();
        }

	}

	public void Initalise()
	{
		foreach (var item in scrollItems)
		{
			item.Initialised();
		}

	}

	// Start is called before the first frame update
	void Start()
    {
		ServiceLocator.Get<RaceManager>().SubscribeResettableObjects((IResettable)this);

	}
}
