using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBrickController : BrickController
{

    public override void OnBallHit(Collision2D other, GameObject ball)
    {
       if(other.gameObject == this.gameObject)
        {
            hitObject = ball;

            //�ı�ǽ��ɫ
            GetComponent<SpriteRenderer>().DOBlendableColor(
             ball.GetComponent<SpriteRenderer>().color, 0.2f);
        }
    }

    public override void OnProjectileHit(Collider2D other, GameObject projectile)
    {
        if (other.gameObject == this.gameObject)
        {
            hitObject = projectile;
            //�ı�ǽ��ɫ
            GetComponent<SpriteRenderer>().DOBlendableColor(
             projectile.GetComponent<SpriteRenderer>().color, 0.2f);
        }
        }
}
