using System;
using UnityEngine;

#pragma warning disable

[RequireComponent(typeof(Animator))]
public sealed class PlayerAnimationController : MonoBehaviour, IModule<PlayerAnimationController>
{
    private Animator _animator;
    private PlayerControlStrategy _mouseControlStrategy;

    private AnimationTriggerFlag _lastUseTriggerAnimation;

    public PlayerAnimationController Init(params object[] args)
    {
        if (args.Length < 1)
            throw new ArgumentException(nameof(args));

        _mouseControlStrategy = (PlayerControlStrategy)args[0];

        if (!gameObject.TryGetComponent(out Animator animator))
            throw new MissingComponentException(nameof(animator));

        _animator = animator;
        _lastUseTriggerAnimation = AnimationTriggerFlag.AnimationWalkDown;

        return this;
    }

    private void Update()
    {
        Vector2 direction = new Vector2(_mouseControlStrategy.Direction.x, _mouseControlStrategy.Direction.y);

        if (Vector2.Distance(direction, transform.position) > 0.1f)
        {
            float angle = Vector2.SignedAngle(Vector2.up, direction);

            ResetLastAnimationTriggerTrigger();

            if (angle < 45 && angle > -45) PlayAnimationByTrigger(AnimationTriggerFlag.AnimationWalkUp);
            else if (angle > 45 && angle < 135) PlayAnimationByTrigger(AnimationTriggerFlag.AnimationWalkLeft);
            else if (angle < -45 && angle > -135) PlayAnimationByTrigger(AnimationTriggerFlag.AnimationWalkRight);
            else PlayAnimationByTrigger(AnimationTriggerFlag.AnimationWalkDown);
        }
    }

    private void ResetLastAnimationTriggerTrigger()
    {
        switch (_lastUseTriggerAnimation)
        {
            case AnimationTriggerFlag.AnimationWalkDown: _animator.ResetTrigger("WalkDown"); break;
            case AnimationTriggerFlag.AnimationWalkRight: _animator.ResetTrigger("WalkRight"); break;
            case AnimationTriggerFlag.AnimationWalkUp: _animator.ResetTrigger("WalkUp"); break;
            case AnimationTriggerFlag.AnimationWalkLeft: _animator.ResetTrigger("WalkLeft"); break;
        }
    }

    private void PlayAnimationByTrigger(AnimationTriggerFlag animationTriggerFlag)
    {
        switch (animationTriggerFlag)
        {
            case AnimationTriggerFlag.AnimationWalkDown:
                _lastUseTriggerAnimation = AnimationTriggerFlag.AnimationWalkDown;
                _animator.SetTrigger("WalkDown"); break;
            case AnimationTriggerFlag.AnimationWalkRight:
                _lastUseTriggerAnimation = AnimationTriggerFlag.AnimationWalkRight;
                _animator.SetTrigger("WalkRight"); break;
            case AnimationTriggerFlag.AnimationWalkUp:
                _lastUseTriggerAnimation = AnimationTriggerFlag.AnimationWalkUp;
                _animator.SetTrigger("WalkUp"); break;
            case AnimationTriggerFlag.AnimationWalkLeft:
                _lastUseTriggerAnimation = AnimationTriggerFlag.AnimationWalkLeft;
                _animator.SetTrigger("WalkLeft"); break;
        }
    }

    public PlayerAnimationController Get()
    {
        return this;
    }
}