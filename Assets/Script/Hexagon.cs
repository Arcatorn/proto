using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hexagon : MonoBehaviour
{
    [SerializeField] Text red;
    [SerializeField] Text green;
    [SerializeField] Text blue;
    float decalage_droite = 0.7f;
    float decalage_haut = 0.4f;

    [SerializeField] GameObject Contour;
    [SerializeField] GameObject Hexagon_full;
	[SerializeField] GameObject player;

    bool activate = false;
    void Start()
    {
    }
    bool can_trigger = true;

    void Update()
    {
        var gauche_X = Input.GetButtonDown("X");
        var bas_A = Input.GetButtonDown("A");
        var droite_B = Input.GetButtonDown("B");
        if (Input.GetAxis("TriggersR") >= 0.3f && can_trigger)
        {
            can_trigger = false;
            master();

        }
        else if (Input.GetAxis("TriggersR") < 0.3f && !can_trigger)
        {
            print("ok");
            can_trigger = true;
        }
        if (activate) deplacementSurHexagon(gauche_X, bas_A, droite_B);

        red.text = "Rouge : " + rouge;
        green.text = "Vert : " + vert;
        blue.text = "Bleu : " + bleu;
    }

    void master()
    {
        if (!activate)
        {
            activate = true;
            Contour.SetActive(true);
            Hexagon_full.SetActive(true);
        }
        else
        {
			set_color();
            activate = false;
            Contour.SetActive(false);
            Hexagon_full.SetActive(false);
            did_blue = false;
            did_green = false;
            did_red = false;
        }
    }
    float rouge = 0;
    bool did_red = false;
    float vert = 0;
    bool did_green = false;

    float bleu = 0;
    bool did_blue = false;


	void set_color()
	{
		Color wantedColor = Color.black;
		wantedColor.b = bleu /255;
		wantedColor.r = rouge /255;
		wantedColor.g = vert/255;

		player.GetComponent<Renderer>().material.color = wantedColor;
	}

    void deplacementSurHexagon(bool gauche_X, bool bas_A, bool droite_B)
    {
        if (gauche_X && bleu < 255 && !(did_green && did_red))
        {
            Contour.transform.Translate(-decalage_droite, decalage_haut, 0);

            if (!did_blue) did_blue = true;
            if (rouge > 0 && vert > 0)
            {
                rouge -= 85;
                vert -= 85;
            }
            else bleu += 85;
        }
        else if (bas_A && vert < 255 && !(did_blue && did_red))
        {
            Contour.transform.Translate(0, -decalage_haut * 2, 0);
            if (!did_green) did_green = true;
            if (rouge > 0 && bleu > 0)
            {
                rouge -= 85;
                bleu -= 85;
            }
            else vert += 85;
        }
        else if (droite_B && rouge < 255 && !(did_green && did_blue))
        {
            Contour.transform.Translate(decalage_droite, decalage_haut, 0);
            if (!did_red) did_red = true;
			 if (bleu > 0 && vert > 0)
            {
                bleu -= 85;
                vert -= 85;
            }
            else rouge += 85;
        }
    }
}
