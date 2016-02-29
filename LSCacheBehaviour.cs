using System;
using UnityEngine;

// Based on Nick Gravelyn's CacheBehaviour (with modifications)
// https://github.com/nickgravelyn/UnityToolbag/blob/master/CacheBehaviour/CacheBehaviour.cs

public abstract class LSCacheBehaviour : MonoBehaviour {
	#region Cached Variables
	[HideInInspector, NonSerialized]
	private Transform _transform;
	public new Transform transform { get { return _transform ? _transform : (_transform = GetComponent<Transform>()); } }


	[HideInInspector, NonSerialized]
	private Animation _animation;
	public new Animation animation { get { return _animation ? _animation : (_animation = GetComponent<Animation>()); } }

	[HideInInspector, NonSerialized]
	private AudioSource _audio;
	public new AudioSource audio { get { return _audio ? _audio : (_audio = GetComponent<AudioSource>()); } }

	[HideInInspector, NonSerialized]
	private Camera _camera;
	public new Camera camera { get { return _camera ? _camera : (_camera = GetComponent<Camera>()); } }

	[HideInInspector, NonSerialized]
	private Collider _collider;
	public new Collider collider { get { return _collider ? _collider : (_collider = GetComponent<Collider>()); } }

	[HideInInspector, NonSerialized]
	private Collider2D _collider2D;
	public new Collider2D collider2D { get { return _collider2D ? _collider2D : (_collider2D = GetComponent<Collider2D>()); } }

	[HideInInspector, NonSerialized]
	private Light _light;
	public new Light light { get { return _light ? _light : (_light = GetComponent<Light>()); } }

	[HideInInspector, NonSerialized]
	private ParticleSystem _particleSystem;
	public new ParticleSystem particleSystem { get { return _particleSystem ? _particleSystem : (_particleSystem = GetComponent<ParticleSystem>()); } }

	[HideInInspector, NonSerialized]
	private Renderer _renderer;
	public new Renderer renderer { get { return _renderer ? _renderer : (_renderer = GetComponent<Renderer>()); } }

	[HideInInspector, NonSerialized]
	private Rigidbody _rigidbody;
	public new Rigidbody rigidbody { get { return _rigidbody ? _rigidbody : (_rigidbody = GetComponent<Rigidbody>()); } }

	[HideInInspector, NonSerialized]
	private Rigidbody2D _rigidbody2D;
	public new Rigidbody2D rigidbody2D { get { return _rigidbody2D ? _rigidbody2D : (_rigidbody2D = GetComponent<Rigidbody2D>()); } }

	[HideInInspector, NonSerialized]
	private SpriteRenderer _spriteRenderer;
	public SpriteRenderer spriteRenderer { get { return _spriteRenderer ? _spriteRenderer : (_spriteRenderer = GetComponent<SpriteRenderer>()); } }
	#endregion
}