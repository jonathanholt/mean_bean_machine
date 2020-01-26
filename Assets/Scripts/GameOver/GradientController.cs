using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientController : MonoBehaviour
{
	
	public Gradient gradient;
	private SpriteRenderer spriteRenderer;
    private float strobeDuration = 2f;
	
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
		float t = Mathf.PingPong(Time.time / this.strobeDuration, 1f);
		spriteRenderer.color = gradient.Evaluate(t);
    }
}
