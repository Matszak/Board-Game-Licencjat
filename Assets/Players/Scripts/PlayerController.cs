using Cards.EnemyCards;
using UnityEngine;
using UnityEngine.Serialization;


public enum PlayerState
{
    None,
    Walking,
    CardPickedUp,
    FightStarted,
    Fighting,
    FightEnded,
    FightWin,
    FightLose,
    Stunned,
}

public class PlayerController : MonoBehaviour
{
    private static readonly int OutLineBool = Shader.PropertyToID("_TurnOn");
    private static readonly int OutLineColor = Shader.PropertyToID("_outLineColor");

    public Player Player { get; set; }

    private PlayerMovement _playerMovement;
    private AdventureCardsChecker _adventureCardsChecker;
    [SerializeField] private DiceRoll diceRoll;

    public PlayerState playerState;
    public PlayerState recentPlayerState;

    // for minus dice roll
    [SerializeField] private bool minusToRoll = false;
    [SerializeField] private int _minusRollValue;

    [SerializeField] private bool _bonusToRoll = false;
    [SerializeField] private int _bonusRollValue;

    [FormerlySerializedAs("_magicShield")] public bool magicShield = false;

    private void OnEnable()
    {
        playerState = PlayerState.None;
        _playerMovement.OnEndMovePlayerMove += CheckIfOnCard;
        Material[] materials = gameObject.GetComponentInChildren<Renderer>().materials;
        materials[1].SetFloat(OutLineBool, 0);
        materials[1].SetColor(OutLineColor, Color.white);
    }

    public void Activate()
    {
        GameManager.Instance.OnFightStarted += ChangeStateToFight;
        GameManager.Instance.TurnStarted += OnTurnStarted;
        CardsOnStart.OnStartCardSelect += OnStartCardSelect;
        FightSystem.EndEnemyFight += OnFightEnded;
    }

    public void Deactivate()
    {
        GameManager.Instance.OnFightStarted -= ChangeStateToFight;
        GameManager.Instance.TurnStarted -= OnTurnStarted;
        CardsOnStart.OnStartCardSelect -= OnStartCardSelect;
        FightSystem.EndEnemyFight -= OnFightEnded;
    }

    private void OnStartCardSelect(Card magicCard)
    {
        Player.playerCards.Add(magicCard);
        CardsOnStart.OnStartCardSelect -= OnStartCardSelect;
    }

    private void OnFightEnded(FightSystem.FightResult fightResult)
    {
        if (Player.currentEnemyCard is BossCard) return;

        //Debug.Log($"player {fightingPlayer}, {fightResult}");
        switch (fightResult)
        {
            case FightSystem.FightResult.Win:
                DebugConsole.Log($"{Player.Name} Wins");
                Player.currentEnemyCard.enemyDefeatedBehaviour.EnemyDefeated();
                //CheckIfOnCard(fightingPlayer);
                break;
            case FightSystem.FightResult.Draw:
                DebugConsole.Log($"{Player.Name} Draw");
                Player.currentEnemyCard.enemyDrawBehaviour.EnemyDraw();
                break;
            case FightSystem.FightResult.Lose:
                DebugConsole.Log($"{Player.Name} Loses");
                Player.currentEnemyCard.enemyWinBehaviour.EnemyWin();
                break;
        }
    }

    private void ChangeStateToFight()
    {
        DebugConsole.Log($"{Player.Name} is attacked by {Player.currentEnemyCard.name}");
        playerState = PlayerState.FightStarted;
    }


    private void CheckIfOnCard()
    {
        recentPlayerState = playerState;

        if (!_adventureCardsChecker.CheckIfStayOnCard() || playerState == PlayerState.Stunned)
        {
            if (playerState == PlayerState.Walking)
            {
                playerState = PlayerState.None;
            }
            if (playerState == PlayerState.Stunned)
            {
                GameManager.Instance.TurnEnded();
            }
            else
            {
                playerState = PlayerState.None;

            }
            GameManager.Instance.TurnEnded();
        }

        else
        {
            AdventureTile adventureTile = _adventureCardsChecker.GetTile();
            GameManager.Instance.CardTriggered(Player, adventureTile);

        }

    }

    [SerializeField] private int stunnedFor;


    [ContextMenu("StunPlayer")]
    public void StunPlayer(int numberOfTurns)
    {
        playerState = PlayerState.Stunned;
        stunnedFor = numberOfTurns;
    }

    private void Awake()
    {
        diceRoll = FindObjectOfType<DiceRoll>();
        _playerMovement = GetComponent<PlayerMovement>();
        _adventureCardsChecker = GetComponent<AdventureCardsChecker>();
    }

    private void OnTurnEnd()
    {
    }

    private void OnTurnStarted()
    {
        switch (playerState)
        {
            case PlayerState.None:
                MovePlayer();
                break;
            case PlayerState.FightStarted:
                Player.currentEnemyCard.TriggerCard(Player);
                break;
            case PlayerState.FightLose:
                Player.currentEnemyCard.TriggerCard(Player);
                break;
            case PlayerState.FightWin:
            case PlayerState.Walking:
            case PlayerState.CardPickedUp:
                MovePlayer();
                break;
            case PlayerState.Stunned:
                if (stunnedFor > 0)
                {
                    stunnedFor--;
                    _playerMovement.MovePlayer(0);
                }
                else
                {
                    MovePlayer();
                }
                break;
        }

    }

    public void MovePlayer()
    {
        playerState = PlayerState.Walking;
        diceRoll.RequestDiceRoll(false, result =>
        {
            int totalMovement = CalculateModifiedRoll(result);
            DebugConsole.Log($"{Player.Name} rolled = {totalMovement}");

            if (totalMovement < 0)
            {
                _playerMovement.MovePlayerBack(Mathf.Abs(totalMovement));
            }
            else
            {
                _playerMovement.MovePlayer(totalMovement);
            }

            ResetModifiers();
        });
    }

    private int CalculateModifiedRoll(int baseResult)
    {
        int total = baseResult;

        if (_bonusToRoll)
        {
            total += _bonusRollValue;
        }

        if (minusToRoll)
        {
            total -= _minusRollValue;
        }

        return total;
    }

    private void ResetModifiers()
    {
        _bonusToRoll = false;
        minusToRoll = false;
        _bonusRollValue = 0;
        _minusRollValue = 0;
    }

    private void OnDisable()
    {
        _playerMovement.OnEndMovePlayerMove -= CheckIfOnCard;
        CardsOnStart.OnStartCardSelect -= OnStartCardSelect;
    }

    public int Attack(int rollResult)
    {
        int attackValue = rollResult;
        if (_bonusToRoll)
        {
            attackValue = rollResult + _bonusRollValue;
            _bonusToRoll = false;
            _bonusRollValue = 0;

        }
        else if (minusToRoll)
        {
            attackValue = rollResult - _minusRollValue;
            minusToRoll = false;
            _minusRollValue = 0;
        }

        return attackValue;
    }

    public void SetMinusDiceRoll(int minusDiceRoll)
    {
        minusToRoll = true;
        _minusRollValue = minusDiceRoll;
    }

    public void SetBonusDiceRoll(int bonusDiceRoll)
    {
        _bonusToRoll = true;
        _bonusRollValue = bonusDiceRoll;
    }

    public void SetMagicShield(bool magicShield)
    {
        this.magicShield = magicShield;
    }
}

