using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
	[Header("Weapon settings")]
	public AudioSource shotSound;
    private Animation anim;
    [HideInInspector]
    public bool canShot=true;
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
	public playerMoving movingScript;
	public GameObject cam;
	
    void Awake() {
     anim = GetComponent<Animation>();
	}
	
    void Update()
    {
		if (Time.timeScale==1&&canShot==true) {
			//Стрельба
			 if (Input.GetMouseButton(0)) {
				shoot();
			 }
         }
		 if (Input.GetMouseButton(1)) {
			 //прицеливание
			 cam.GetComponent<Camera>().fieldOfView=aimPower;
			 //movingScript.lookSpeed=movingScript.startlookSpeed/4;
		 }
		 else {
			 //отмена прицеливания
			 cam.GetComponent<Camera>().fieldOfView=60;
			// movingScript.lookSpeed=movingScript.startlookSpeed;
		 }
		}
		
    private IEnumerator reload() {
        anim.clip = idleAnim;
        anim.Play();
		yield return new WaitForSeconds(reloadingTime);
        canShot=true;
    }

	private void shoot() {
		RaycastHit hit;
		shotDirectrion=cam.transform.TransformDirection(Vector3.forward);
		canShot=false;
		shotVFX.Play();
		shotSound.Play();
		anim.clip = shotAnim;
		anim.Play();
		foreach (AnimationState state in anim) state.speed = animSpeed;
		Debug.DrawRay(transform.position, shotDirectrion*range, Color.red, 3.0f);
		Physics.Raycast(cam.gameObject.transform.position, shotDirectrion, out hit, range);
		if (hit.collider!=null&&hit.collider.gameObject.layer==6) {
			//если цель
			GameObject hitted=hit.transform.gameObject;
			//hitted.GetComponent<enemyHealth>().health-=damage;
		}
		StartCoroutine(reload());
	}
}
