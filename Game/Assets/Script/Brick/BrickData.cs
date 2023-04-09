using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BrickData
{
    public int count = 1;
    [HideInInspector]
    public int maxCount;
    public Color brickColor;
    
    /// <summary>
    /// ����ש��
    /// </summary>
    /// <param name="currentBrick">��ǰש��</param>
    public void UpdateBrick(GameObject currentBrick)
    {
        var brickSpriteRenderer = currentBrick.GetComponent<SpriteRenderer>();
        if (brickSpriteRenderer == null) return;
        //������ɫ
        brickSpriteRenderer.color = brickColor;
        brickSpriteRenderer.color = new Color(brickSpriteRenderer.color.r, brickSpriteRenderer.color.g, brickSpriteRenderer.color.b, count / (float)maxCount);
        if (brickSpriteRenderer.color.a == 0) brickSpriteRenderer.color = brickSpriteRenderer.color = new Color(brickSpriteRenderer.color.r, brickSpriteRenderer.color.g, brickSpriteRenderer.color.b, 0.1f);
    }
}