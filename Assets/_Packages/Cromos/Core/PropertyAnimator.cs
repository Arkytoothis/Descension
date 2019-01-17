/********************************************************************
	created:	2016/12/13
	file base:	PropertyAnimator
	file ext:	cs
	author:		Alessandro Maione
	version:	1.6.0
	
	purpose:	a generic property animator
*********************************************************************/
using UnityEngine;

namespace Cromos
{
    [System.Serializable]
    /// <summary>
    /// a generic property animator
    /// </summary>
    /// <typeparam name="T">type of the property to be animated</typeparam>
    public abstract class PropertyAnimator<T> : MonoBehaviour
    {
        [Header( "Transition" )]
        /// <summary>
        /// Duration in seconds of the transition
        /// </summary>
        [Tooltip( "Duration in seconds of the transition" )]
        public float Duration = 1; // secs
                                   /// <summary>
                                   /// Delay in seconds before transition will start
                                   /// </summary>
        [Tooltip( "Delay in seconds before transition will start" )]
        public float Delay = 0; // secs

        /// <summary>
        /// Loop policy
        /// </summary>
        [Tooltip( "Loop policy" )]
        public TransitionLoopModes LoopMode = TransitionLoopModes.PlayOnce;
        /// <summary>
        /// What to do after animation is complete. Reset will reset start values, Clamp will keep last calculated ones
        /// </summary>
        [Tooltip( "What to do after animation is complete. Reset will reset start values, Clamp will keep last calculated ones" )]
        public WrapModes WrapMode = WrapModes.Clamp;
        /// <summary>
        /// If true, the transition will be played in reverse order (colors and alpha values of Colors property will be fetched from end to beginning) 
        /// This flag is automatically inverted when ping-pong loop mode is set
        /// </summary>
        [Tooltip( "If true, the transition will be played in reverse order (colors and alpha values of Colors property will be fetched from end to beginning) " )]
        [SerializeField]
        private bool reversed = false;
        public bool Reversed
        {
            get
            { return reversed; }
        }


        [Header( "Options" )]
        /// <summary>
        /// if true, the transition will be propagated to all the objects in the hierarchy
        /// </summary>
        [Tooltip( "if true, the transition will be propagated to all the objects in the hierarchy" )]
        public bool AnimateChildren = false;
        protected bool animateChildren = false;
        /// <summary>
        /// Defines when the transition will automatically start. If it is set to None, the transition will not start automatically
        /// </summary>
        [Tooltip( "Defines when the transition will automatically start. If it is set to None, the transition will not start automatically" )]
        public PropertyStartupModes StartTransitionMode = PropertyStartupModes.OnEnable;
        /// <summary>
        /// action to perform after transition
        /// </summary>
        [Tooltip( "action to perform after transition" )]
        public AfterTransitionActions AfterTransition = AfterTransitionActions.DoNothing;

        [Header( "State (read only)" )]
        /// <summary>
        /// Flag that tells if a transition is currently playing
        /// </summary>
        [Tooltip( "Flag that tells if a transition is currently playing" )]
        public bool InProgress = false;
        /// <summary>
        /// Flag that tells if the current animation loop is complete
        /// </summary>
        [Tooltip( "Flag that tells if the current animation loop is complete" )]
        public bool AnimationComplete = false;

        [Header( "Events" )]
        /// <summary>
        /// event to be set in editor or by script. The event callbacks will be called when a transition starts
        /// </summary>
        [Tooltip( "event to be set in editor or by script. The event callbacks will be called when a transition starts" )]
        public PropertyAnimatorEvent OnStart;
        /// <summary>
        /// event to be set in editor or by script. The event callbacks will be called when a transition ends
        /// </summary>
        [Tooltip( "event to be set in editor or by script. The event callbacks will be called when a transition ends" )]
        public PropertyAnimatorEvent OnEnd;

        /// <summary>
        /// elapsed time (in seconds) from when the transition started
        /// </summary>
        public float ElapsedTime
        {
            get
            {
                float res = 0;

                if ( Reversed )
                    res = reverseTime - Time.time + startTime;
                else
                    res = Time.time - startTime;

                res -= Delay;

                res = Mathf.Clamp( res, 0, Duration );

                return res;
            }
        }
        /// <summary>
        /// normalized Elapsed Time value ([0..1])
        /// </summary>
        public float ElapsedTimeNormalized
        {
            get
            {
                return Mathf.Clamp01( ElapsedTime / Duration );
            }
        }

        public virtual float FirstIndex
        {
            get
            {
                return 0;
            }
        }
        public virtual float LastIndex
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// allows to get or set the first color in Colors gradient (alpha will be ignored)
        /// </summary>
        public T StartValue
        {
            get
            {
                // if ( Colors == null ) Colors = new Gradient();
                return EvaluateValue( Reversed ? LastIndex : FirstIndex );
            }
            set
            {
                //if ( Colors == null ) Colors = new Gradient();
                SetValue( Reversed ? LastIndex : FirstIndex, value );
            }
        }
        /// <summary>
        /// allows to get or set the last color in Colors gradient (alpha will be ignored)
        /// </summary>
        public T EndValue
        {
            get
            {
                // if ( Colors == null ) Colors = new Gradient();
                return EvaluateValue( Reversed ? FirstIndex : LastIndex );
            }
            set
            {
                //if ( Colors == null ) Colors = new Gradient();
                SetValue( Reversed ? FirstIndex : LastIndex, value );
            }
        }
        /// <summary>
        /// the current color used for animation from Colors gradient (alpha is ignored)
        /// </summary>
        public T CurrentValue
        {
            get
            {
                return EvaluateValue( ElapsedTimeNormalized );
            }
        }


        protected float startTime = 0;
        protected float reverseTime = 0;
        protected bool restartOnComplete = true;

        void Awake()
        {
            animateChildren = AnimateChildren;
            SetReversed( false );
        }

        protected virtual void OnEnable()
        {
            if ( StartTransitionMode == PropertyStartupModes.OnEnable )
                StartTransition();
        }

        protected virtual void OnDisable()
        {
            StopTransition();
        }

        protected virtual void Start()
        {
            if ( StartTransitionMode == PropertyStartupModes.OnStart )
                StartTransition();
        }

        /// <summary>
        /// starts the property transition (animation) using the properties set for the PropertyAnimator object
        /// </summary>
        public virtual void StartTransition()
        {
            AnimationComplete = false;
            startTime = Time.time;
            InProgress = true;

            if ( OnStart != null )
                OnStart.Invoke();

            enabled = true;
        }

        public virtual void PauseTransition()
        {
            InProgress = false;
            enabled = true;
        }

        public virtual void ResumeTransition()
        {
            InProgress = true;
            enabled = true;
        }

        public virtual void ReverseTransition()
        {
            SetReversed( true );
        }

        public virtual void ResetReverseTransition()
        {
            SetReversed( false );
        }

        public virtual void InvertTransition()
        {
            SetReversed( !Reversed );
        }

        public virtual void StopTransition( bool executeAfterTransitionOperations = true )
        {
            AnimationComplete = true;
            InProgress = false;
            if ( executeAfterTransitionOperations )
                ManageAfterTransition();
        }

        protected virtual void Update()
        {
            if ( !InProgress )
                return;

            if ( AnimateChildren != animateChildren )
                OnAnimateChildrenChanged();

            ManagePropertyAnimation();
        }

        protected abstract T EvaluateValue( float toEvaluate );
        protected abstract void SetValue( float t, T valueToBeSet );
        protected abstract void SetValue( int idx, T valueToBeSet );
        protected abstract void ResetValues();

        public void SetReversed( bool reversedVal )
        {
            if ( reversedVal )
                reverseTime = ElapsedTime;
            else
                reverseTime = 0;

            reversed = reversedVal;
            startTime = Time.time;
        }

        protected virtual void OnAnimateChildrenChanged()
        {
            animateChildren = AnimateChildren;
        }

        protected virtual bool ManagePropertyAnimation()
        {
            if ( AnimationComplete )
                return true;

            if ( Time.time - startTime >= Delay )
            {
                if ( Reversed )
                    AnimationComplete = ( ElapsedTime <= 0 );
                else
                    AnimationComplete = ( ElapsedTime >= Duration );

                if ( AnimationComplete )
                {
                    InProgress = false;

                    if ( OnEnd != null )
                        OnEnd.Invoke();

                    switch ( LoopMode )
                    {
                        case TransitionLoopModes.PlayOnce:
                            ManageAfterTransition();
                            break;
                        case TransitionLoopModes.PingPong:
                            InProgress = true;
                            InvertTransition();
                            if ( restartOnComplete )
                                StartTransition();
                            else
                                StopTransition();
                            break;
                        case TransitionLoopModes.Repeat:
                            if ( restartOnComplete )
                                StartTransition();
                            else
                                StopTransition();
                            break;
                        default:
                            Debug.LogWarning( "unknown loop mode" );
                            break;
                    }
                }
            }
            return AnimationComplete;
        }

        protected virtual void ManageAfterTransition()
        {
            switch ( WrapMode )
            {
                case WrapModes.Restore:
                    ResetValues();
                    break;
                default:
                    // nothing to do. Last values will be automatically preserved
                    ;
                    break;
            }

            switch ( AfterTransition )
            {
                case AfterTransitionActions.DoNothing:
                    ;
                    break;
                case AfterTransitionActions.DeactivateGameObject:
                    enabled = false;
                    gameObject.SetActive( false );
                    break;
                case AfterTransitionActions.DestroyGameObject:
                    enabled = false;
                    Destroy( gameObject );
                    break;
                case AfterTransitionActions.DisableComponent:
                    enabled = false;
                    break;
                case AfterTransitionActions.DestroyComponent:
                    Destroy( this );
                    break;
                default:
                    Debug.LogError( "you should never read me!" );
                    break;
            }
        }

        #region DEBUG
        public override string ToString()
        {
            return base.ToString() + " on '" + gameObject.name + "'";
        }
        #endregion

    }
}

//TODO: gestire metodi diversi di ease per l'animazione della proprietà


