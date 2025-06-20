using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayImage : MonoBehaviour
{
    [SerializeField] Texture texture;

    // Start is called before the first frame update
    void Start()
    {
        using (AndroidJavaClass pluginClass = new AndroidJavaClass("com.example.unitymodule.unityModule"))
        {
            var unityModule = pluginClass.CallStatic<AndroidJavaObject>("instance");

            Debug.Log($"unityModule: {unityModule}");

            //launcher = pluginClass.CallStatic<AndroidJavaObject>("getInstance", context);
            byte[] bytes = unityModule.Call<byte[]>("getIcon", 0);
            Debug.Log($"bytes: {bytes}");

            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(bytes);
            //texture.GetComponent<Renderer>().material.mainTexture = tex;
            //texture = tex;
            GetComponent<RawImage>().texture = tex;
        }


        //Bytes2TexTest();

        //DisplayImage di = new DisplayImage();
        //Bytes2Tex(GameManager.Instance?.PluginInstance?.Get<byte[]>("tex_bytes"));
    }


    public void Set_Webcam_Image(byte[] recevByteArr)
    {
        try
        {
            Texture2D bmp;
            bmp = new Texture2D(8, 8);
            //bmp.LoadRawTextureData(recevBuffer);
            bmp.LoadImage(recevByteArr, false);

            //Vector2 pivot = new Vector2(0.5f, 0.5f);
            //Rect tRect = new Rect(0, 0, bmp.width, bmp.height);
            //Sprite newSprite = Sprite.Create(bmp, tRect, pivot);

            //GameObject webcam = GameObject.Find("Webcam");
            GetComponent<RawImage>().texture = bmp; 

            //webcam.GetComponent<Image>().overrideSprite = newSprite;

        }
        catch
        {
        }
    }

    public void Bytes2Tex(byte[] pvrtcBytes)
    {
        // Create a 16 x 16 texture with PVRTC RGBA4 format and fill it with raw PVRTC bytes.
        Texture2D tex = new Texture2D(16, 16, TextureFormat.PVRTC_RGBA4, false);

        // Raw PVRTC4 data for a 16 x 16 texture.
        // This format is four bits per pixel, so the data should be 128 (16 x 16 / 2) bytes in size.
        // The texture encoded here is mostly green with some angular blue and red lines.
        //byte[] pvrtcBytes = new byte[]
        //{
        //    0x30, 0x32, 0x32, 0x32, 0xe7, 0x30, 0xaa, 0x7f, 0x32, 0x32, 0x32, 0x32, 0xf9, 0x40, 0xbc, 0x7f,
        //    0x03, 0x03, 0x03, 0x03, 0xf6, 0x30, 0x02, 0x05, 0x03, 0x03, 0x03, 0x03, 0xf4, 0x30, 0x03, 0x06,
        //    0x32, 0x32, 0x32, 0x32, 0xf7, 0x40, 0xaa, 0x7f, 0x32, 0xf2, 0x02, 0xa8, 0xe7, 0x30, 0xff, 0xff,
        //    0x03, 0x03, 0x03, 0xff, 0xe6, 0x40, 0x00, 0x0f, 0x00, 0xff, 0x00, 0xaa, 0xe9, 0x40, 0x9f, 0xff,
        //    0x5b, 0x03, 0x03, 0x03, 0xca, 0x6a, 0x0f, 0x30, 0x03, 0x03, 0x03, 0xff, 0xca, 0x68, 0x0f, 0x30,
        //    0xaa, 0x94, 0x90, 0x40, 0xba, 0x5b, 0xaf, 0x68, 0x40, 0x00, 0x00, 0xff, 0xca, 0x58, 0x0f, 0x20,
        //    0x00, 0x00, 0x00, 0xff, 0xe6, 0x40, 0x01, 0x2c, 0x00, 0xff, 0x00, 0xaa, 0xdb, 0x41, 0xff, 0xff,
        //    0x00, 0x00, 0x00, 0xff, 0xe8, 0x40, 0x01, 0x1c, 0x00, 0xff, 0x00, 0xaa, 0xbb, 0x40, 0xff, 0xff,
        //};

        // Load data into the texture and upload it to the GPU.
        tex.LoadRawTextureData(pvrtcBytes);
        tex.Apply();

        // Assign the texture to this GameObject's material.
        GetComponent<RawImage>().texture = tex;
    }

    private void Bytes2TexTest()
    {
        // Create a 16 x 16 texture with PVRTC RGBA4 format and fill it with raw PVRTC bytes.
        Texture2D tex = new Texture2D(16, 16, TextureFormat.PVRTC_RGBA4, false);

        // Raw PVRTC4 data for a 16 x 16 texture.
        // This format is four bits per pixel, so the data should be 128 (16 x 16 / 2) bytes in size.
        // The texture encoded here is mostly green with some angular blue and red lines.
        byte[] pvrtcBytes = new byte[]
        {
            0x30, 0x32, 0x32, 0x32, 0xe7, 0x30, 0xaa, 0x7f, 0x32, 0x32, 0x32, 0x32, 0xf9, 0x40, 0xbc, 0x7f,
            0x03, 0x03, 0x03, 0x03, 0xf6, 0x30, 0x02, 0x05, 0x03, 0x03, 0x03, 0x03, 0xf4, 0x30, 0x03, 0x06,
            0x32, 0x32, 0x32, 0x32, 0xf7, 0x40, 0xaa, 0x7f, 0x32, 0xf2, 0x02, 0xa8, 0xe7, 0x30, 0xff, 0xff,
            0x03, 0x03, 0x03, 0xff, 0xe6, 0x40, 0x00, 0x0f, 0x00, 0xff, 0x00, 0xaa, 0xe9, 0x40, 0x9f, 0xff,
            0x5b, 0x03, 0x03, 0x03, 0xca, 0x6a, 0x0f, 0x30, 0x03, 0x03, 0x03, 0xff, 0xca, 0x68, 0x0f, 0x30,
            0xaa, 0x94, 0x90, 0x40, 0xba, 0x5b, 0xaf, 0x68, 0x40, 0x00, 0x00, 0xff, 0xca, 0x58, 0x0f, 0x20,
            0x00, 0x00, 0x00, 0xff, 0xe6, 0x40, 0x01, 0x2c, 0x00, 0xff, 0x00, 0xaa, 0xdb, 0x41, 0xff, 0xff,
            0x00, 0x00, 0x00, 0xff, 0xe8, 0x40, 0x01, 0x1c, 0x00, 0xff, 0x00, 0xaa, 0xbb, 0x40, 0xff, 0xff,
        };

        // Load data into the texture and upload it to the GPU.
        tex.LoadRawTextureData(pvrtcBytes);
        tex.Apply();

        // Assign the texture to this GameObject's material.
        GetComponent<RawImage>().texture = tex;
    }
}
