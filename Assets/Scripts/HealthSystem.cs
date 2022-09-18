using UnityEngine;

public class HealthSystem : MonoBehaviour
{
	private int health = 100;
	public int Health
	{
		get => health;
		set
		{
			health -= value;
			if (health >= 0) return;
			health = 0;
			Death();
		}
	}
	
	[SerializeField]
	private AnimationClip dieAnim;
	private Animation anim;
	
	private void Awake() 
	{
		anim = GetComponent<Animation>();
	}

	private void Death()
	{
		anim.clip = dieAnim;
		anim.Play();
		Destroy(this);
	}
}
