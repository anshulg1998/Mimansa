using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxScroll : MonoBehaviour, IResettable
{
	public Transform cameraTransform;
	public float parallaxSpeed = 0.5f; // lower = slower movement = more distant
	private Vector3 lastCameraPosition;
	private float textureUnitSizeX;

	private Vector3 startPosition;
	private float startPos;
	public void Reset()
	{
		transform.position = startPosition;
		startPos = startPosition.x;
		initialised = false;
	}

	private bool initialised = false;
	public void Initialised()
	{
		initialised = true;
	}

	void Start()
	{
		startPosition = transform.position;
		startPos = startPosition.x;
		lastCameraPosition = cameraTransform.position;

		Sprite sprite = GetComponent<SpriteRenderer>().sprite;
		Texture2D texture = sprite.texture;
		textureUnitSizeX = sprite.bounds.size.x;
	}

	void Update()
	{
		if (initialised)
		{
			float temp = cameraTransform.position.x * (1 - parallaxSpeed);
			float dist = cameraTransform.position.x * parallaxSpeed;
			transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

			if (temp > startPos + textureUnitSizeX) { startPos += textureUnitSizeX; }
			//else if (temp < startPos + textureUnitSizeX) { startPos -= textureUnitSizeX; }
		}

	}
}
