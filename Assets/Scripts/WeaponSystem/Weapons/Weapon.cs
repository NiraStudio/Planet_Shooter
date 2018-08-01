using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {


    [SerializeField]
    protected GameObject Bullet;
    [SerializeField]
    protected Transform shootPos;
    protected SFX sfx;
    protected PowerUpManager PUM;
    public float Damage
    {
        get
        {
            float multi=1;
            if (PUM != null)
                multi += (PUM.IsActive(PowerUpType.DoubleAttack) ? 1 : 0);
            return _damage*multi;
        }
        set
        {
            _damage = value;
        }
    }
    float _damage;

    // This Var Show CharacterHolder Direction
    public int Direction
    {
        set { _direction = value; }
        get { return _direction; }
    }
    int _direction=-1;


    // This Var Show Regenerate Speed Direction
    public float RegenerateSpeed
    {
        set { _speed = value; }
        get { return _speed; }
    }
    float _speed;

    // This Vars Show Ammo Of Direction

    public float Ammo
    {
        get
        {
            return _ammo;
        }
        set
        {
            _ammo = value;
        }
    }
    public float MaxAmmo
    {
        get
        {
            return _maxAmmo;
        }
        set
        {
            _maxAmmo = value;
        }
    }


    float _ammo, _maxAmmo;

    protected CharacterHolder CH;
	// Use this for initialization
	void Start () {
        PUM = PowerUpManager.Instance;
        _ammo = _maxAmmo;
        sfx = GetComponent<SFX>();
	}

    protected virtual void Update()
    {
        if (GamePlayManager.GPM == null)
            return;
        if (GamePlayManager.GPM.gamePlayState != GamePlayState.Play)
            return;

        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
                OnTouchBegin();
            if (Input.GetMouseButton(0))
                OnTouchStationary();

        }

        #region MobileController
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 1)
        {
            Touch tch = Input.GetTouch(0);
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(tch.fingerId))
                switch (tch.phase)
                {
                    case TouchPhase.Began:
                        OnTouchBegin();
                        break;
                    case TouchPhase.Moved:
                        OnTouchMoved();
                        break;
                    case TouchPhase.Stationary:
                        OnTouchStationary();
                        break;
                    case TouchPhase.Ended:
                        OnTouchEnded();
                        break;
                    case TouchPhase.Canceled:
                        OnTouchCanceled();
                        break;
                    default:
                        break;
                }
        }
#endif
        #endregion

        AmmoRegenerationMethod();
    }

    // This Methods Called By Touching

    protected virtual void OnTouchBegin() { }
    protected virtual void OnTouchMoved() { }
    protected virtual void OnTouchStationary() { }
    protected virtual void OnTouchEnded() { }
    protected virtual void OnTouchCanceled() { }



 
   


    //Weapon Can Recharge By Pickables With These Method

    public void GetAmmo(float Amount)
    {
        _ammo += Amount;
        if (_ammo > MaxAmmo)
            _ammo = MaxAmmo;
    }

    //This Method is Use for Reagenarate Method

    protected virtual void AmmoRegenerationMethod()
    {

    }

}
