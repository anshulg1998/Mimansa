using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxScroll : MonoBehaviour, IResettable
{
	public Transform cameraTransform;
	public float parallaxSpeed = 0.5f; // lower = slower movement = more distant
	private Vector3 lastCameraPosition;
	private float textureUnitSizeX;

	private Vector3 startPosition;
	public void Reset()
	{
		transform.position = startPosition;
	}

	void Start()
	{
		startPosition = transform.position;

		lastCameraPosition = cameraTransform.position;

		Sprite sprite = GetComponent<SpriteRenderer>().sprite;
		Texture2D texture = sprite.texture;
		textureUnitSizeX = texture.width / sprite.pixelsPerUnit * transform.localScale.x;
	}

	void Update()
	{
		Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
		transform.position += new Vector3(deltaMovement.x * parallaxSpeed, 0f, 0f);
		lastCameraPosition = cameraTransform.position;

		// Loop the sprite
		float offsetX = cameraTransform.position.x - transform.position.x;
		float height = Camera.main.orthographicSize * 2f;
		float width = height * Camera.main.aspect;
		if (Mathf.Abs(offsetX) + width/2 >= textureUnitSizeX )
		{
			float offsetPosX = (offsetX % textureUnitSizeX);
			transform.position = new Vector3(cameraTransform.position.x + offsetPosX, transform.position.y);
		}
	}
}
