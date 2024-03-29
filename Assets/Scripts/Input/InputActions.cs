//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Input/InputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""GameMap"",
            ""id"": ""9ca24593-0d71-4564-ba92-d2d46bde3588"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""384bf965-4488-4de1-86b6-f3878c14a51c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""b639583b-9ab8-49b2-b614-94497a27e363"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Recall"",
                    ""type"": ""Button"",
                    ""id"": ""b5761fda-24d8-4187-9f60-117ba953d5be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSkillA"",
                    ""type"": ""Button"",
                    ""id"": ""deb95175-8dc7-4c18-b615-f92b0adf9410"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSkillB"",
                    ""type"": ""Button"",
                    ""id"": ""5194743a-efb4-4929-b7b8-bf5727e32046"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSkillC"",
                    ""type"": ""Button"",
                    ""id"": ""1fadb7d5-f883-49da-8471-cf7498ee9638"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectSkillD"",
                    ""type"": ""Button"",
                    ""id"": ""3976d98f-07ce-4151-8ac7-895b39bed298"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""1477bd83-fdf6-4f8f-acfe-9b0e7382b639"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""63deedcc-e38b-4062-a264-213fb25fbc87"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c39d9a72-b18e-4345-a91f-9474743d945d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4a2bb952-20ae-42c8-98f8-76d7661f207f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d67464fa-b7c8-4ef0-940e-10f18f2f583d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fef9e0c7-edf8-47aa-9669-f3f407b89397"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fefad1c2-975f-4c87-a94a-104779d261c2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Recall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""713a2401-ba7a-4bc7-a164-6609e5182f87"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSkillA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80fcfb39-dfbf-4839-b299-967015b11403"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSkillB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7414a294-91fc-49ce-a14a-0b49145bfc56"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSkillC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""844c2e8d-56ac-4940-9bc3-ce78935c884d"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSkillD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // GameMap
        m_GameMap = asset.FindActionMap("GameMap", throwIfNotFound: true);
        m_GameMap_Move = m_GameMap.FindAction("Move", throwIfNotFound: true);
        m_GameMap_Fire = m_GameMap.FindAction("Fire", throwIfNotFound: true);
        m_GameMap_Recall = m_GameMap.FindAction("Recall", throwIfNotFound: true);
        m_GameMap_SelectSkillA = m_GameMap.FindAction("SelectSkillA", throwIfNotFound: true);
        m_GameMap_SelectSkillB = m_GameMap.FindAction("SelectSkillB", throwIfNotFound: true);
        m_GameMap_SelectSkillC = m_GameMap.FindAction("SelectSkillC", throwIfNotFound: true);
        m_GameMap_SelectSkillD = m_GameMap.FindAction("SelectSkillD", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // GameMap
    private readonly InputActionMap m_GameMap;
    private List<IGameMapActions> m_GameMapActionsCallbackInterfaces = new List<IGameMapActions>();
    private readonly InputAction m_GameMap_Move;
    private readonly InputAction m_GameMap_Fire;
    private readonly InputAction m_GameMap_Recall;
    private readonly InputAction m_GameMap_SelectSkillA;
    private readonly InputAction m_GameMap_SelectSkillB;
    private readonly InputAction m_GameMap_SelectSkillC;
    private readonly InputAction m_GameMap_SelectSkillD;
    public struct GameMapActions
    {
        private @InputActions m_Wrapper;
        public GameMapActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_GameMap_Move;
        public InputAction @Fire => m_Wrapper.m_GameMap_Fire;
        public InputAction @Recall => m_Wrapper.m_GameMap_Recall;
        public InputAction @SelectSkillA => m_Wrapper.m_GameMap_SelectSkillA;
        public InputAction @SelectSkillB => m_Wrapper.m_GameMap_SelectSkillB;
        public InputAction @SelectSkillC => m_Wrapper.m_GameMap_SelectSkillC;
        public InputAction @SelectSkillD => m_Wrapper.m_GameMap_SelectSkillD;
        public InputActionMap Get() { return m_Wrapper.m_GameMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameMapActions set) { return set.Get(); }
        public void AddCallbacks(IGameMapActions instance)
        {
            if (instance == null || m_Wrapper.m_GameMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameMapActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Fire.started += instance.OnFire;
            @Fire.performed += instance.OnFire;
            @Fire.canceled += instance.OnFire;
            @Recall.started += instance.OnRecall;
            @Recall.performed += instance.OnRecall;
            @Recall.canceled += instance.OnRecall;
            @SelectSkillA.started += instance.OnSelectSkillA;
            @SelectSkillA.performed += instance.OnSelectSkillA;
            @SelectSkillA.canceled += instance.OnSelectSkillA;
            @SelectSkillB.started += instance.OnSelectSkillB;
            @SelectSkillB.performed += instance.OnSelectSkillB;
            @SelectSkillB.canceled += instance.OnSelectSkillB;
            @SelectSkillC.started += instance.OnSelectSkillC;
            @SelectSkillC.performed += instance.OnSelectSkillC;
            @SelectSkillC.canceled += instance.OnSelectSkillC;
            @SelectSkillD.started += instance.OnSelectSkillD;
            @SelectSkillD.performed += instance.OnSelectSkillD;
            @SelectSkillD.canceled += instance.OnSelectSkillD;
        }

        private void UnregisterCallbacks(IGameMapActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Fire.started -= instance.OnFire;
            @Fire.performed -= instance.OnFire;
            @Fire.canceled -= instance.OnFire;
            @Recall.started -= instance.OnRecall;
            @Recall.performed -= instance.OnRecall;
            @Recall.canceled -= instance.OnRecall;
            @SelectSkillA.started -= instance.OnSelectSkillA;
            @SelectSkillA.performed -= instance.OnSelectSkillA;
            @SelectSkillA.canceled -= instance.OnSelectSkillA;
            @SelectSkillB.started -= instance.OnSelectSkillB;
            @SelectSkillB.performed -= instance.OnSelectSkillB;
            @SelectSkillB.canceled -= instance.OnSelectSkillB;
            @SelectSkillC.started -= instance.OnSelectSkillC;
            @SelectSkillC.performed -= instance.OnSelectSkillC;
            @SelectSkillC.canceled -= instance.OnSelectSkillC;
            @SelectSkillD.started -= instance.OnSelectSkillD;
            @SelectSkillD.performed -= instance.OnSelectSkillD;
            @SelectSkillD.canceled -= instance.OnSelectSkillD;
        }

        public void RemoveCallbacks(IGameMapActions instance)
        {
            if (m_Wrapper.m_GameMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameMapActions instance)
        {
            foreach (var item in m_Wrapper.m_GameMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameMapActions @GameMap => new GameMapActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface IGameMapActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnRecall(InputAction.CallbackContext context);
        void OnSelectSkillA(InputAction.CallbackContext context);
        void OnSelectSkillB(InputAction.CallbackContext context);
        void OnSelectSkillC(InputAction.CallbackContext context);
        void OnSelectSkillD(InputAction.CallbackContext context);
    }
}
