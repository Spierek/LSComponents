using System;
using UnityEngine;

// Based on Nick Gravelyn's CacheBehaviour (with modifications)
// https://github.com/nickgravelyn/UnityToolbag/blob/master/CacheBehaviour/CacheBehaviour.cs

// using bool checks instead of null checks to avoid Object.op_Implicit() overhead

public abstract class LSCacheBehaviour : MonoBehaviour {
	#region Cached Variables
	[HideInInspector, NonSerialized]
	private Transform _transform;
	private bool _isTransformSet;
	public new Transform transform {
		get {
			if (!_isTransformSet) {
				_transform = GetComponent<Transform>();
				_isTransformSet = true;
			}

			return _transform;
		}
	}


	[HideInInspector, NonSerialized]
	private Animation _animation;
	private bool _isAnimationSet;
	public new Animation animation {
		get {
			if (!_isAnimationSet) {
				_animation = GetComponent<Animation>();
				_isAnimationSet = true;
			}

			return _animation;
		}
	}

	[HideInInspector, NonSerialized]
	private AudioSource _audio;
	private bool _isAudioSet;
	public new AudioSource audio {
		get {
			if (!_isAudioSet) {
				_audio = GetComponent<AudioSource>();
				_isAudioSet = true;
			}

			return _audio;
		}
	}

	[HideInInspector, NonSerialized]
	private Camera _camera;
	private bool _isCameraSet;
	public new Camera camera {
		get {
			if (!_isCameraSet) {
				_camera = GetComponent<Camera>();
				_isCameraSet = true;
			}

			return _camera;
		}
	}

	[HideInInspector, NonSerialized]
	private Collider _collider;
	private bool _isColliderSet;
	public new Collider collider {
		get {
			if (!_isColliderSet) {
				_collider = GetComponent<Collider>();
				_isColliderSet = true;
			}

			return _collider;
		}
	}

	[HideInInspector, NonSerialized]
	private Collider2D _collider2D;
	private bool _isCollider2DSet;
	public new Collider2D collider2D {
		get {
			if (!_isCollider2DSet) {
				_collider2D = GetComponent<Collider2D>();
				_isCollider2DSet = true;
			}

			return _collider2D;
		}
	}

	[HideInInspector, NonSerialized]
	private Light _light;
	private bool _isLightSet;
	public new Light light {
		get {
			if (!_isLightSet) {
				_light = GetComponent<Light>();
				_isLightSet = true;
			}

			return _light;
		}
	}

	[HideInInspector, NonSerialized]
	private ParticleSystem _particleSystem;
	private bool _isParticleSystemSet;
	public new ParticleSystem particleSystem {
		get {
			if (!_isParticleSystemSet) {
				_particleSystem = GetComponent<ParticleSystem>();
				_isParticleSystemSet = true;
			}

			return _particleSystem;
		}
	}

	[HideInInspector, NonSerialized]
	private Renderer _renderer;
	private bool _isRendererSet;
	public new Renderer renderer {
		get {
			if (!_isRendererSet) {
				_renderer = GetComponent<Renderer>();
				_isRendererSet = true;
			}

			return _renderer;
		}
	}

	[HideInInspector, NonSerialized]
	private Rigidbody _rigidbody;
	private bool _isRigidbodySet;
	public new Rigidbody rigidbody {
		get {
			if (!_isRigidbodySet) {
				_rigidbody = GetComponent<Rigidbody>();
				_isRigidbodySet = true;
			}

			return _rigidbody;
		}
	}

	[HideInInspector, NonSerialized]
	private Rigidbody2D _rigidbody2D;
	private bool _isRigidbody2DSet;
	public new Rigidbody2D rigidbody2D {
		get {
			if (!_isRigidbody2DSet) {
				_rigidbody2D = GetComponent<Rigidbody2D>();
				_isRigidbody2DSet = true;
			}

			return _rigidbody2D;
		}
	}

	[HideInInspector, NonSerialized]
	private SpriteRenderer _spriteRenderer;
	private bool _isSpriteRendererSet;
	public SpriteRenderer spriteRenderer {
		get {
			if (!_isSpriteRendererSet) {
				_spriteRenderer = GetComponent<SpriteRenderer>();
				_isSpriteRendererSet = true;
			}

			return _spriteRenderer;
		}
	}
	#endregion
}