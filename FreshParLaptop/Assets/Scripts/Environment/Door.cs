using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Door : NetworkBehaviour
{

private Rigidbody rb;
private Animator animator;
private NetworkAnimator networkAnimator;

private HingeJoint joint;

private bool opened;
private bool locked;

public AudioClip nClose; public AudioClip nOpen;
public AudioClip hClose; public AudioClip hOpen;

public AudioClip lockedSound;


[Server]
void Start() {
    rb = GetComponent<Rigidbody>();
    joint = GetComponent<HingeJoint>();
    animator = GetComponent<Animator>();
    networkAnimator = GetComponent<NetworkAnimator>();
}
[Server]
public void OpenDoor(float force) 
{
 if (joint.limits.max == 0 )
 {
    if (joint.angle > joint.limits.min * 0.65f)
        rb.AddForce( transform.forward* -force);
    else
        rb.AddForce(transform.forward * force);


 }
 else
 {
     if (joint.angle < joint.limits.max * 0.65f)
        rb.AddForce(transform.forward * force);
    else
        rb.AddForce(transform.forward * -force);


 }
    
}

[Server]
private void OnTriggerEnter(Collider other) {
    if (other.tag == "Trigger" )
    {
         AudioSource audioSource;
        if (other.name == "DoorClose")
        {
            audioSource = other.GetComponent<AudioSource>();
            if (rb.velocity.magnitude<=1.5f)
                audioSource.clip = nClose;
            else
                audioSource.clip = hClose;
            audioSource.Play();
        }

    }
}

[Server]
private void OnTriggerExit(Collider other) {

    if (other.tag == "Trigger" )
    {
        AudioSource audioSource;
        if (other.name == "DoorClose")
        {
            
            audioSource = other.GetComponent<AudioSource>();
            if (rb.velocity.magnitude<=1.5f)
                audioSource.clip = nOpen;
            else
                audioSource.clip = hOpen;
            audioSource.Play();

        }
    }
}

[Server]
public void LockTheDoor()
{

}


}
