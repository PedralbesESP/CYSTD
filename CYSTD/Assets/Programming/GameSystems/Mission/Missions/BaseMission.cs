using UnityEngine;

public class BaseMission : MonoBehaviour
{
    [SerializeField]
    bool enabledOnStart = true;
    [SerializeField]
    protected string _explaination;
    protected MissionState _state;
    protected bool _enabled;
    [SerializeField] protected MissionName _name;

    public virtual bool IsCompleted() { return _state == MissionState.DONE; }
    public virtual bool IsFailed() { return _state == MissionState.FAILED; }

    void Awake()
    {
        if (enabledOnStart) Enable();
        else Disable();
    }

    protected virtual bool UpdateValidations()
    {
        if (!_enabled) return false;
        if (IsCompleted()) return false;
        return true;
    }

    public MissionName getName()
    {
        return _name;
    }

    public void SetMissionState(MissionState state)
    {
        if (_enabled)
        {
            _state = state;
            NetworkManager.Instance.MissionChangeState(this);
        }
    }

    public MissionState GetMissionState()
    {
        return _state;
    }

    public virtual string GetMissionExplaination()
    {
        return _explaination;
    }

    public virtual bool IsEnabled()
    {
        return _enabled;
    }

    public virtual void Enable()
    {
        _enabled = true;
    }

    public virtual void Disable()
    {
        _enabled = false;
    }

    public void _CompleteMission()
    {
        SetMissionState(MissionState.DONE);
        Disable();
    }

    public virtual void ResetMission()
    {
        SetMissionState(MissionState.NOT_DONE);
    }
}