using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {


    public Transform[] Tausta;     // arraay taustoillle 
    private float[] parallaxScales; // liikkumiis osuus kameralle
    public float smoothing = 1f; // pehmennetään liikettä

    private Transform cam; //pääkameran viite
    private Vector3 previousCamPos; // kameran aiempi paikka x, y ja z akselieilla

    void Awake()
    {
        cam = Camera.main.transform;

    }


	void Start () {
        //Edellisen framin kamera paikka = nykyinen
        previousCamPos = cam.position;
        
        parallaxScales = new float[Tausta.Length];

            for (int i = 0; i < Tausta.Length; i++) //käydään kaikki Tausta läpi
        {
            parallaxScales[i] = Tausta[i].position.z * -1;
        }
        	
	}//start
	

	void Update () {
        for (int i = 0; i < Tausta.Length; i++) { //Tausta läpi
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i]; //kameroiden paikkojen erolla tehdään parallax efekti

            float TaustaargetPosX = Tausta[i].position.x + parallax;
            Vector3 TaustaargetPos = new Vector3 (TaustaargetPosX, Tausta[i].position.y, Tausta[i].position.z);

            // fadaus 
            Tausta[i].position = Vector3.Lerp(Tausta[i].position, TaustaargetPos, smoothing * Time.deltaTime);

        }

        previousCamPos = cam.position;
	}//update
}//class
