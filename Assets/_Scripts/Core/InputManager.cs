using System;
using System.Collections.Generic;
using JustGame.Scripts.Runtime;
using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public enum BindingAction
    {
        MOVE_LEFT = 0,
        MOVE_RIGHT,
        MOVE_UP,
        MOVE_DOWN,
        DASH,
        
        SHOOT_LEFT,
        SHOOT_RIGHT,
        SHOOT_UP,
        SHOOT_DOWN,
        USE_ACTIVE_ABILITY = 10,
    }
    [Serializable]
    public struct ButtonAssign
    {
        public BindingAction keyAction;
        public KeyCode keyCode;
    }
    
    public enum ButtonStates
    {
        ButtonClicked,
        ButtonReleased,
        ButtonDown,
    }

    public class InputManager : PersistentSingleton<InputManager>
    {
        [SerializeField] private GlobalRuntimeVariables m_runtimeVariables;
        public ButtonAssign[] buttonsAssigns;
   
        private Dictionary<BindingAction, ButtonStates> m_buttonDictionary;
        private Dictionary<BindingAction, ButtonStates> m_mouseBtnDictionary;
        private bool m_isInputActive;

        public Action<Vector2> RightClickDownCallback;
        public Action<Vector2> RightClickUpCallback;
        
        public bool IsInputActive
        {
            get
            {
                return m_isInputActive;
            }
            set
            {
                m_isInputActive = value;
                for (int i = 0; i < buttonsAssigns.Length; i++)
                {
                    m_buttonDictionary[buttonsAssigns[i].keyAction] = ButtonStates.ButtonReleased;
                }
            }
        }
        protected override void Awake()
        {
            base.Awake();
            m_buttonDictionary = new Dictionary<BindingAction, ButtonStates>();
            
            for (int i = 0; i < buttonsAssigns.Length; i++)
            {
                m_buttonDictionary.Add(buttonsAssigns[i].keyAction,ButtonStates.ButtonReleased);
            }

            IsInputActive = true;
        }
        
        private void Update()
        {
            if (!IsInputActive) return;
            for (int i = 0; i < buttonsAssigns.Length; i++)
            {
                if (Input.GetKeyDown(buttonsAssigns[i].keyCode))
                {
                    m_buttonDictionary[buttonsAssigns[i].keyAction] = ButtonStates.ButtonClicked;
                }
                else if (Input.GetKey(buttonsAssigns[i].keyCode))
                {
                    m_buttonDictionary[buttonsAssigns[i].keyAction] = ButtonStates.ButtonDown;
                }
                else if (Input.GetKeyUp(buttonsAssigns[i].keyCode))
                {
                    m_buttonDictionary[buttonsAssigns[i].keyAction] = ButtonStates.ButtonReleased;
                }
            }
        }

        #region Keyboards
        public bool GetKeyClicked(BindingAction keyAction)
        {
            return (m_buttonDictionary[keyAction] == ButtonStates.ButtonClicked) && IsInputActive;
        }
        public bool GetKeyUp(BindingAction keyAction)
        {
            return (m_buttonDictionary[keyAction] == ButtonStates.ButtonReleased) && IsInputActive;
        }

        public bool GetKeyDown(BindingAction keyAction)
        {
            return (m_buttonDictionary[keyAction] == ButtonStates.ButtonDown) && IsInputActive;
        }
        #endregion
        
        #region Mouse
        public bool GetLeftClick()
        {
            return IsInputActive && Input.GetMouseButton(0);
        }
        
        public bool GetLeftClickDown()
        {
            return IsInputActive && Input.GetMouseButtonDown(0);
        }
        
        public bool GetLeftClickUp()
        {
            return IsInputActive && Input.GetMouseButtonUp(0);
        }
        
        public bool GetRightClick()
        {
            return IsInputActive && Input.GetMouseButton(1);
        }
        
        public bool GetRightClickDown()
        {
            if (IsInputActive && Input.GetMouseButtonDown(1))
            {
                RightClickDownCallback?.Invoke(Input.mousePosition);
                return true;
            }
            return false;
        }
        
        public bool GetRightClickUp()
        {
            if (IsInputActive && Input.GetMouseButtonUp(1))
            {
                RightClickUpCallback?.Invoke(Input.mousePosition);
                return true;
            }
            return false;
        }

        public Vector3 GetWorldMousePos()
        {
            var pos = m_runtimeVariables.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            return pos;
        }
        
        #endregion

        public void Reset()
        {
            for (int i = 0; i < buttonsAssigns.Length; i++)
            {
                if (Input.GetKeyDown(buttonsAssigns[i].keyCode))
                {
                    m_buttonDictionary[buttonsAssigns[i].keyAction] = ButtonStates.ButtonClicked;
                }
                else if (Input.GetKey(buttonsAssigns[i].keyCode))
                {
                    m_buttonDictionary[buttonsAssigns[i].keyAction] = ButtonStates.ButtonDown;
                }
                else if (Input.GetKeyUp(buttonsAssigns[i].keyCode))
                {
                    m_buttonDictionary[buttonsAssigns[i].keyAction] = ButtonStates.ButtonReleased;
                }
            }
        }
    }
}

