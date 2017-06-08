using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnnemy : MonoBehaviour {

    //public GameObject[] buttonRef;

    [HideInInspector]
    public List<CombinationHandler.Button> Combination = new List<CombinationHandler.Button>();
    public int CombinationSize = 3;
    public int Life = 2;
    public float Speed = 0.1f;
    public bool IsMoving = true;
    public int NbrGold = 0;
    public float slow = 1;
    [HideInInspector]
    public Vector3 Position;
    [HideInInspector]
    public bool reverse = false;
    public float SpawnCooldown = 1000.0f;
    public bool Died = false;
    
    protected GameManager Gm;
    protected Dictionary<CombinationHandler.Button, GameObject> ObjectToInstantiate = new Dictionary<CombinationHandler.Button, GameObject>();
    protected BoxCollider boxCollider;
    protected List<GameObject> ButtonsEnemy = new List<GameObject>();
	protected GameObject EnemyLifeObj;

    protected int CurrentCombo = 0;
    public GameObject ParticleSystem;

    protected void Awake()
    { 
        ObjectToInstantiate[CombinationHandler.Button.BLUE] = Resources.Load<GameObject>("Prefabs/ButtonsEnemy/BlueButton");
        ObjectToInstantiate[CombinationHandler.Button.YELLOW] = Resources.Load<GameObject>("Prefabs/ButtonsEnemy/YellowButton");
        ObjectToInstantiate[CombinationHandler.Button.GREEN] = Resources.Load<GameObject>("Prefabs/ButtonsEnemy/GreenButton");
        ObjectToInstantiate[CombinationHandler.Button.RED] = Resources.Load<GameObject>("Prefabs/ButtonsEnemy/RedButton");
		EnemyLifeObj = Resources.Load<GameObject>("Prefabs/EnemyLife");
        boxCollider = GetComponent<BoxCollider>();
        Position = GetComponent<Transform>().position;
        ParticleSystem = Resources.Load<GameObject>("Prefabs/ButtonsEnemy/ParticleSystem");
    }

    public virtual List<CombinationHandler.Button> getCombination()
    {
        return Combination;
    }

    protected void Start()
    {
        Gm = GameManager.instance;
        Init(Life,NbrGold,CombinationSize);
    }

    void Start (List<CombinationHandler.Button> combination, int nbrGold, Vector3 position) {
        Combination = combination;
        NbrGold = nbrGold;
        Position = position;
        Gm = GameManager.instance;
    }

    public virtual void Init(int nLife, int nGold, int nConbinationCount)
    {
        this.Life = nLife;
        this.NbrGold = nGold;
        this.CombinationSize = nConbinationCount;
    }

    protected virtual void Attack()
    {
        foreach (GameObject b in ButtonsEnemy)
        {
            b.SetActive(false);
        }
        EnemyLifeObj.SetActive(false);
        IsMoving = false;

        Gm.RemoveLife(1);

		GetComponent<Animator> ().SetTrigger("Attack");
		Gm.NotifyDie(gameObject, false);
    }

    public virtual void Die()
    {
        foreach (GameObject b in ButtonsEnemy)
        {
            b.SetActive(false);
        }
        EnemyLifeObj.SetActive(false);
        IsMoving = false;

		GetComponent<Animator> ().SetTrigger("Die");
		Gm.NotifyDie(gameObject, true);
    }
    
	public virtual int DecreaseLifePoint(int lp)
	{
        int ret = 0;
        if (lp > 0)
        {
            ret = Life > lp ? lp : Life;
            Life = Life > lp ? Life - lp : 0;
            EnemyLifeObj.GetComponent<TextMesh>().text = Life.ToString();
            DoParticle();
            if (Life <= 0)
            {
                Die();
            }
        }
        return ret;
    }

    protected virtual void Move()
    {
        if (IsMoving)
        {
            Position.y -= (Speed * slow) * (reverse ? -1 : 1);
            GetComponent<Transform>().position = new Vector3(Position.x, Position.y, Position.z);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EndObject")
            Attack();
    }

	public void ResetCombination()
	{
        foreach (GameObject b in ButtonsEnemy)
        {
            Destroy(b);
        }
		ButtonsEnemy.Clear ();

		float ysize = GetComponent<BoxCollider>().size.y;
		float ycenter = GetComponent<BoxCollider>().center.y;
		float yscale = GetComponent<Transform>().localScale.y;

		float xsize = GetComponent<BoxCollider>().size.x;
		float xcenter = GetComponent<BoxCollider>().center.x;
		float xscale = GetComponent<Transform>().localScale.x;

		int count = 1;
		Vector3 buttonPos;
		foreach (CombinationHandler.Button b in Combination)
		{
			buttonPos = Position;

			GameObject button = Instantiate(ObjectToInstantiate[b], Position, Quaternion.identity);
			button.GetComponent<SpriteRenderer>().enabled = true;

			float ybuttonsize = button.GetComponent<SpriteRenderer>().sprite.bounds.size.y * button.transform.localScale.y;
			float xbuttonsize = button.GetComponent<SpriteRenderer>().sprite.bounds.size.x * button.transform.localScale.x;

			buttonPos.x -= (xsize * (xscale / 2)) + (xcenter * xscale) + (xbuttonsize / 2);
			buttonPos.y -= (ysize * (yscale / 2)) + (ycenter * yscale) - (ybuttonsize / 2);
			buttonPos.y += ((count -1) * ybuttonsize) * 0.75f;

			// Code to align button on the top

			/*Vector3 buttonPosition = Position;
            buttonPosition.y += boxCollider.size.y * 0.9f;

            float buttonSizeX = button.GetComponent<SpriteRenderer>().sprite.bounds.size.x * button.transform.localScale.x * 0.65f;
            float positionButtonCompare = - (float)CombinationSize * (float)buttonSizeX * 0.5f; 
            positionButtonCompare += buttonSizeX * count - buttonSizeX * 0.5f;
            buttonPosition.x += positionButtonCompare;*/

			button.transform.position = buttonPos;
			button.transform.SetParent(gameObject.transform);

			count += 1;

			ButtonsEnemy.Add(button);
		}
	}

    public void Setup()
    {
		float ysize = GetComponent<BoxCollider>().size.y;
		float ycenter = GetComponent<BoxCollider>().center.y;
		float yscale = GetComponent<Transform>().localScale.y;

		float xsize = GetComponent<BoxCollider>().size.x;
		float xcenter = GetComponent<BoxCollider>().center.x;
		float xscale = GetComponent<Transform>().localScale.x;

        int count = 1;
		Vector3 buttonPos;
        foreach (CombinationHandler.Button b in Combination)
        {
			buttonPos = Position;

            GameObject button = Instantiate(ObjectToInstantiate[b], Position, Quaternion.identity);
            button.GetComponent<SpriteRenderer>().enabled = true;

			float ybuttonsize = button.GetComponent<SpriteRenderer>().sprite.bounds.size.y * button.transform.localScale.y;
			float xbuttonsize = button.GetComponent<SpriteRenderer>().sprite.bounds.size.x * button.transform.localScale.x;
				
			buttonPos.x -= (xsize * (xscale / 2)) + (xcenter * xscale) + (xbuttonsize / 2);
			buttonPos.y -= (ysize * (yscale / 2)) + (ycenter * yscale) - (ybuttonsize / 2);
			buttonPos.y += ((count -1) * ybuttonsize) * 0.75f;

			// Code to align button on the top

            /*Vector3 buttonPosition = Position;
            buttonPosition.y += boxCollider.size.y * 0.9f;

            float buttonSizeX = button.GetComponent<SpriteRenderer>().sprite.bounds.size.x * button.transform.localScale.x * 0.65f;
            float positionButtonCompare = - (float)CombinationSize * (float)buttonSizeX * 0.5f; 
            positionButtonCompare += buttonSizeX * count - buttonSizeX * 0.5f;
            buttonPosition.x += positionButtonCompare;*/

            button.transform.position = buttonPos;
            button.transform.SetParent(gameObject.transform);

            count += 1;

            ButtonsEnemy.Add(button);
        }

		// Setup Enemy life

		Vector3 lifePos = Position;

		lifePos.x += (xsize * (xscale / 2)) + (xcenter * xscale);
		lifePos.y += (ysize * (yscale / 2)) + (ycenter * yscale);

		EnemyLifeObj = Instantiate(EnemyLifeObj, lifePos, Quaternion.identity);
		EnemyLifeObj.transform.SetParent(gameObject.transform);

		// Change render order
		Renderer liferend = EnemyLifeObj.GetComponent<MeshRenderer>();
		liferend.sortingLayerName = "Entity";
		liferend.sortingOrder = 100;

		EnemyLifeObj.GetComponent<TextMesh> ().text = Life.ToString();

    }

    // Update is called once per frame
    protected virtual void Update () {
        if (Gm.isPaused)
            return;
        Move();
	}

    public void UpdateWithConfig(ConfigurationEnemy setting)
    {
        CombinationSize = setting.CombinationSize;
        Speed = setting.Speed;
        NbrGold = setting.Gold;
        SpawnCooldown = setting.SpawnCoolDown;
        Life = setting.Life;
    }

    public void FeedBackCombination(CombinationHandler CombinationPlayer, bool reset = false)
    {
        if (!reset)
        {
            int count = 0;
            foreach (CombinationHandler.Button currentButton in CombinationPlayer.GetCurrentCombination())
            {
                ButtonsEnemy[count].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
                count++;
            }

            if (CurrentCombo < count)
                DoParticle();

            CurrentCombo = count;
        }
        else
        {
            CurrentCombo = 0;
            for (int i = 0; i < ButtonsEnemy.Count; i++)
                ButtonsEnemy[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    public void DoParticle()
    {
        GameObject particle = Instantiate(ParticleSystem, transform.position, Quaternion.identity);
        particle.transform.parent = transform;
        Destroy(particle, 1.0f);
    }
}
