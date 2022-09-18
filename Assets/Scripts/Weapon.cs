using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Header("Weapon settings")]
	public AudioSource shotSound;
    private Animation anim;
    [HideInInspector]
    public bool canShoot=true;
    public int damage=15;
	public int range=300;
    public float animSpeed=1.3f;
	public float reloadingTime=0.0f;
    public ParticleSystem shotVFX;
    public AnimationClip shotAnim;
    public AnimationClip idleAnim;
	public int aimPower=40;
	private Vector3 shotDirectrion;
	
	[Header("Player settings")]
	public Transform cam;
	
    void Awake() 
    {
		anim = GetComponent<Animation>();
	}
	
    private IEnumerator Reload() 
    {
	    //anim.clip = idleAnim;
	    //anim.Play();
	    yield return new WaitForSeconds(reloadingTime);
	    canShoot = true;
    }

    public void Shoot()
    {
	    shotDirectrion = cam.TransformDirection(Vector3.forward);
	    shotVFX.Play();
	    shotSound.Play();
	    // anim.clip = shotAnim;
	    // anim.Play();
	    // foreach (AnimationState state in anim) state.speed = animSpeed;
	    Debug.DrawRay(transform.position, shotDirectrion * range, Color.red, 3.0f);
	    Physics.Raycast(cam.gameObject.transform.position, shotDirectrion, out var hit, range);
	    if (hit.collider != null && hit.transform.gameObject.TryGetComponent<HealthSystem>(out var hittable))
	    {
		    hittable.GetDamage(damage);
	    }

	    canShoot = false;
	    StartCoroutine(Reload());
    }
}
