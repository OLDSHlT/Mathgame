using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathStickLoader : MonoBehaviour
{
    public List<Sprite> verticalSticks;
    public List<Sprite> horizontalSticks;
    public Sprite emptyStick;
    // Start is called before the first frame update
    void Start()
    {
        emptyStick = LoadSpriteFromPath("Assets/Art/MathSticks/empty.png");
        verticalSticks = new List<Sprite>();
        horizontalSticks = new List<Sprite>();

        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/1-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/2-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/3-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/4-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/5-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/6-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/7-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/8-vertical.png"));
        verticalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/9-vertical.png"));

        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/1-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/2-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/3-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/4-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/5-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/6-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/7-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/8-horizontal.png"));
        horizontalSticks.Add(LoadSpriteFromPath("Assets/Art/MathSticks/9-horizontal.png"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 根据文件路径加载图片为 Sprite 对象
    private Sprite LoadSpriteFromPath(string path)
    {
        // 使用 Unity 的 API 加载图片
        Texture2D texture = LoadTextureFromPath(path);
        if (texture != null)
        {
            // 创建 Sprite 对象
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
        return null;
    }

    // 根据文件路径加载图片为 Texture2D 对象
    private Texture2D LoadTextureFromPath(string path)
    {
        // 使用 Unity 的 API 加载图片
        Texture2D texture = null;
        byte[] fileData;

        if (System.IO.File.Exists(path))
        {
            fileData = System.IO.File.ReadAllBytes(path);
            texture = new Texture2D(2, 2);
            texture.LoadImage(fileData); // 自动设置图片尺寸
        }
        else
        {
            Debug.LogError("File not found: " + path);
        }

        return texture;
    }
}
