using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour
{

    SpriteRenderer origTile;

    void Start()
    {
        origTile = GetComponent<SpriteRenderer>();
        //Vector2 tileSize = new Vector2(origTile.bounds.size.x/transform.localScale.x, origTile.bounds.size.y/transform.localScale.y);

        // Set up tile to be repeated
        GameObject baseTile = new GameObject();
        baseTile.name = "spriteTile";
        baseTile.transform.position = transform.position;
        if ((int)Mathf.Round(transform.localScale.x) % 2 == 0)
            baseTile.transform.position -= (new Vector3(0.5f, 0, 0) * transform.localScale.x / 2) - new Vector3(0.25f, 0, 0);
        else
            baseTile.transform.position -= (new Vector3(0.5f, 0, 0) * Mathf.Floor(transform.localScale.x / 2));
        if ((int)Mathf.Round(transform.localScale.y) % 2 == 0)
            baseTile.transform.position -= (new Vector3(0, 0.5f, 0) * transform.localScale.y / 2) - new Vector3(0, 0.25f, 0);
        else
            baseTile.transform.position -= (new Vector3(0, 0.5f, 0) * Mathf.Floor(transform.localScale.y / 2));

        SpriteRenderer baseSprite = baseTile.AddComponent<SpriteRenderer>();
        baseSprite.sprite = origTile.sprite;
        baseSprite.color = origTile.color;
        baseSprite.drawMode = SpriteDrawMode.Tiled;
        baseSprite.tileMode = SpriteTileMode.Adaptive;

        // Create tiles x
        GameObject tiles;
        for (int i = 1; i < (int)Mathf.Round(transform.localScale.x); ++i)
        {
            tiles = Instantiate(baseTile);
            tiles.transform.position = baseTile.transform.position + (new Vector3(0.5f, 0, 0) * i);
            tiles.transform.parent = transform;
        }

        // Create tiles y
        for (int i = 1; i < (int)Mathf.Round(transform.localScale.y); ++i)
        {
            tiles = Instantiate(baseTile);
            tiles.transform.position = baseTile.transform.position + (new Vector3(0, 0.5f, 0) * i);
            tiles.transform.parent = transform;
        }

        // Set the parent last on the prefab to prevent transform displacement
        baseTile.transform.parent = transform;

        // Disable the currently existing sprite component since its now a repeated image
        origTile.enabled = false;
    }
}