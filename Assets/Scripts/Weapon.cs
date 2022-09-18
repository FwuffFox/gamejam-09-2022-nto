using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Header("Weapon settings")]
	public AudioSource shotSound;
    private Animation anim;
    [HideInInspector]
    public bool canShoot=true;
	[HideInInspector]
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
	public Camera cam;
	
    void Awake() 
    {
		anim = GetComponent<Animation>();
	}
	
    private IEnumerator Reload() 
    {
	    anim.clip = idleAnim;
	    anim.Play();
	    yield return new WaitForSeconds(reloadingTime);
	    canShoot = true;
    }

    public void Shoot() 
    {
	    shotDirectrion = cam.transform.TransformDirection(Vector3.forward);
	    shotVFX.Play();
	    shotSound.Play();
	    anim.clip = shotAnim;
	    anim.Play();
	    foreach (AnimationState state in anim) state.speed = animSpeed;
	    Debug.DrawRay(transform.position, shotDirectrion * range, Color.red, 3.0f);
	    Physics.Raycast(cam.gameObject.transform.position, shotDirectrion, out var hit, range);
	    if (hit.collider!=null&&hit.collider.gameObject.layer==6) {
		    //если цель
		    GameObject hitted = hit.transform.gameObject;
		    //hitted.GetComponent<enemyHealth>().health-=damage;
	    }
	    canShoot = false;
	    StartCoroutine(Reload());
    }
    
}
