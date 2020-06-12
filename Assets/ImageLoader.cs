using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class ImageLoader : MonoBehaviour
{

    public Dictionary<string, Sprite> AllSprites = new Dictionary<string, Sprite>(); //Store all Sprites created into this dictionary container
    string artPath = "";
    public string m_Path;
    public Image logo;
    public Text debg;
    void Start()
    {
       m_Path = Application.dataPath;
        artPath = m_Path + "\\src";
        LoadAllSpritesFromPngFilesInFolderAndAllSubFolders();
        debg.text = artPath;
        Sprite sp = GetSprite("logo");
        logo.sprite = sp;
    }
    void LoadAllSpritesFromPngFilesInFolderAndAllSubFolders()
    {
        //Get all files PNG in "ART" directory
        string[] allFilePaths = Directory.GetFiles(artPath, "*.png", SearchOption.TopDirectoryOnly);

        foreach (string filePath in allFilePaths)
        {
            byte[] newPngFileData;
            newPngFileData = File.ReadAllBytes(filePath);

            Texture2D newTexture2D = new Texture2D(2, 2);
            newTexture2D.LoadImage(newPngFileData);

            Sprite newSprite = Sprite.Create(newTexture2D, new Rect(0, 0, newTexture2D.width, newTexture2D.height), new Vector2(0, 0), 1);
            string spriteName = Path.GetFileNameWithoutExtension(filePath);
            AllSprites.Add(spriteName, newSprite);


        }
       // Debug.Log("Finished Loading Sprites! Total Sprites Loaded: " + AllSprites.Count);
    }

    public Sprite GetSprite(string imageName)
    {
      //  Debug.Log(AllSprites[imageName] + "Haamadada");

        //Debug.Log("Getting Sprite:'" + imageName + "'");
        if (!AllSprites.ContainsKey(imageName))
        {
            //Trim excess Space
            imageName = imageName.Trim();
            //Try Again
           
            if (!AllSprites.ContainsKey(imageName))
            {
                Debug.LogError("SPRITE NOT FOUND in AllSprites:[" + imageName + "]");
                return AllSprites["Error"];
            }
        }
        return AllSprites[imageName];
    }
}
