using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAbilityLevel : MonoBehaviour {
   
    private SpriteRenderer rendererButton;

	// Use this for initialization
	void Start () {
        rendererButton = GameObject.Find("AbilityButtonVisible").gameObject.GetComponent<SpriteRenderer>();
        GameData d = PersistantData.instance.data;
        Sprite sprite;

        switch (d.PowerUpUsing)
        {
            case GameData.PowerUp.PAWN:
                GameManager.instance.powerUp = GameManager.instance.gameObject.AddComponent<PawnPowerUp>();
                GameManager.instance.powerUp.Level = d.CurrentPowerUp[(int)GameData.PowerUp.PAWN];
                sprite = Resources.Load<Sprite>("UI/finalPower/pawn");
                break;
            case GameData.PowerUp.BISHOP:
                GameManager.instance.powerUp = GameManager.instance.gameObject.AddComponent<BishopPowerUp>();
                GameManager.instance.powerUp.Level = d.CurrentPowerUp[(int)GameData.PowerUp.BISHOP];
                sprite = Resources.Load<Sprite>("UI/finalPower/bishop");
                break;
            case GameData.PowerUp.KNIGHT:
                GameManager.instance.powerUp = GameManager.instance.gameObject.AddComponent<KnightPowerUp>();
                GameManager.instance.powerUp.Level = d.CurrentPowerUp[(int)GameData.PowerUp.KNIGHT];
                sprite = Resources.Load<Sprite>("UI/finalPower/knight");
                break;
            case GameData.PowerUp.ROOK:
                GameManager.instance.powerUp = GameManager.instance.gameObject.AddComponent<RookPowerUp>();
                GameManager.instance.powerUp.Level = d.CurrentPowerUp[(int)GameData.PowerUp.ROOK];
                sprite = Resources.Load<Sprite>("UI/finalPower/rook");
                break;
            case GameData.PowerUp.QUEEN:
                GameManager.instance.powerUp = GameManager.instance.gameObject.AddComponent<QueenPowerUp>();
                GameManager.instance.powerUp.Level = d.CurrentPowerUp[(int)GameData.PowerUp.QUEEN];
                sprite = Resources.Load<Sprite>("UI/finalPower/queen");
                break;
            case GameData.PowerUp.KING:
                GameManager.instance.powerUp = GameManager.instance.gameObject.AddComponent<KingPowerUp>();
                GameManager.instance.powerUp.Level = d.CurrentPowerUp[(int)GameData.PowerUp.KING];
                sprite = Resources.Load<Sprite>("UI/finalPower/king");
                break;
            case GameData.PowerUp.NONE:
            default:
                sprite = Resources.Load<Sprite>("UI/PowerUpCircle");
                break;
        }

        rendererButton.sprite = sprite;
    }
}
