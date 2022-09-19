using UnityEngine;

public class HealthSystem : MonoBehaviour
{
	[SerializeField] public int health = 100;

	[SerializeField]
	private AnimationClip dieAnim;
	private Animation anim;
	
	private void Awake() 
	{
		anim = GetComponent<Animation>();
	}

	public void GetDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			health = 0;
			Death();
		}
	}
	private void Death()
	{
		//anim.clip = dieAnim;
		//anim.Play();
		print($"{gameObject.name} died");
		Destroy(gameObject, 1);
	}
}
