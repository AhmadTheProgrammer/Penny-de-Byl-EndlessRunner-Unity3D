using System.Collections.Generic;
using UnityEngine;
public class DestroyWall : MonoBehaviour
{
    public GameObject[] bricks;
    List<Rigidbody> bricksRBs = new List<Rigidbody>();
    List<Vector3> positions = new List<Vector3>();
    List<Quaternion> rotations = new List<Quaternion>();
    public GameObject explosion; // for putting in our particle system

    Collider col;

    void OnEnable()
    {
        col.enabled = true;
        for (int i = 0; i < bricks.Length; i++)
        {
            bricks[i].transform.localPosition = positions[i];
            bricks[i].transform.localRotation = rotations[i];
            bricksRBs[i].isKinematic = true;
        }
    }


    void Awake()
    {
        col = this.GetComponent<Collider>();
        foreach (GameObject b in bricks)
        {
            bricksRBs.Add(b.GetComponent<Rigidbody>());
            positions.Add(b.transform.localPosition);
            rotations.Add(b.transform.localRotation);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Spell")
        {
            // turning on the particle system for the wall explosion
            GameObject obj=Instantiate(explosion,other.contacts[0].point,Quaternion.identity);
            Destroy(obj, 2.5f);
            col.enabled = false;
            foreach (Rigidbody r in bricksRBs)
                r.isKinematic = false;

            //play sounds
            PlayerController.sfx[3].Play();

        }
    }
}