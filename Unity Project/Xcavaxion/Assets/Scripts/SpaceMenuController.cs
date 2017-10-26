using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //GameObject menuImage = CenterMenuImage();
        //SetTitle(menuImage);
        //SetStartButton(menuImage);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    private GameObject CenterMenuImage()
    {
        int sWidth = Screen.width;
        int sHeight = Screen.height;
        GameObject bImage = GameObject.Find("BImage");
        RectTransform rt = (RectTransform)bImage.transform;
        var width = rt.rect.width / 2;
        var height = rt.rect.height / 2;

        Vector3 pos = bImage.transform.position;
        pos.x = (sWidth / 2);// -width;
        pos.y = (sHeight / 2);// +height;
        bImage.transform.position = pos;

        return bImage;
    }

    private void SetStartButton(GameObject menuImage)
    {
        var widthCenter = menuImage.transform.position.x;
        var heightCenter = menuImage.transform.position.y;

        GameObject sButton = GameObject.Find("SButton");

        Vector3 pos = sButton.transform.position;
        pos.x = widthCenter;
        //Bump from bottom
        RectTransform rt = (RectTransform)menuImage.transform;
        var menuImageHeight = rt.rect.height;
        var bump = (float)(menuImageHeight * .9);
        pos.y = heightCenter - bump;
        sButton.transform.position = pos;

    }

    public void SetTitle(GameObject menuImage) {
        var widthCenter = menuImage.transform.position.x;
        var heightCenter = menuImage.transform.position.y;

        RectTransform rt = (RectTransform)menuImage.transform;
        var menuImageHeightTop = heightCenter + (rt.rect.height / 2);

        GameObject sButton = GameObject.Find("TitleText");

        Vector3 pos = sButton.transform.position;
        //pos.x = ;
        pos.y = menuImageHeightTop;
        sButton.transform.position = pos;
    }

    //int level is the index of the level in the build settings
    public void LoadSpaceScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
