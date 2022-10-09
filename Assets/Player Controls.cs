//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Player Controls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Controls"",
    ""maps"": [
        {
            ""name"": ""CharacterControls"",
            ""id"": ""12a53abd-2c09-4e5b-86aa-528508662f02"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""9f96cae5-f313-40e3-86b7-2bc167fc6f97"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""7eabafc4-c22a-445d-99b5-7d44e6e1069f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""4cc5a46c-ae74-41a5-aa9d-64c134e29add"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""26b23a31-d87d-44e8-ae8e-539419429b96"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""238dc447-ee1d-4204-8fcf-e74fb21cea63"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6c38e52d-eca5-4783-af84-7cc4a212ea6a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a8ad9c0e-1667-400f-b25d-3704361704d6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3c007f8c-db97-47d0-849d-aba43592f20e"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UserInterface"",
            ""id"": ""a64d53d7-d2cd-4011-810f-32bf9ada8cbd"",
            ""actions"": [
                {
                    ""name"": ""InventoryScreen"",
                    ""type"": ""Button"",
                    ""id"": ""2742f7c7-ce80-45ce-b1ae-1c86d1708c71"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EquipmentScreen"",
                    ""type"": ""Button"",
                    ""id"": ""10b93436-f316-4235-82ca-d021d4b3ea5f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AttributeScreen"",
                    ""type"": ""Button"",
                    ""id"": ""62fdb4ca-8227-4828-9b53-674170dcd22b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f3487af5-0b22-4718-a756-a89d2deb7407"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InventoryScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46494775-e014-4914-8e11-eeb14b8a332f"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EquipmentScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f4dbb27-f217-41d1-acd9-56a4cc951b23"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttributeScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterControls
        m_CharacterControls = asset.FindActionMap("CharacterControls", throwIfNotFound: true);
        m_CharacterControls_Move = m_CharacterControls.FindAction("Move", throwIfNotFound: true);
        m_CharacterControls_Run = m_CharacterControls.FindAction("Run", throwIfNotFound: true);
        // UserInterface
        m_UserInterface = asset.FindActionMap("UserInterface", throwIfNotFound: true);
        m_UserInterface_InventoryScreen = m_UserInterface.FindAction("InventoryScreen", throwIfNotFound: true);
        m_UserInterface_EquipmentScreen = m_UserInterface.FindAction("EquipmentScreen", throwIfNotFound: true);
        m_UserInterface_AttributeScreen = m_UserInterface.FindAction("AttributeScreen", throwIfNotFound: true);
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

    // CharacterControls
    private readonly InputActionMap m_CharacterControls;
    private ICharacterControlsActions m_CharacterControlsActionsCallbackInterface;
    private readonly InputAction m_CharacterControls_Move;
    private readonly InputAction m_CharacterControls_Run;
    public struct CharacterControlsActions
    {
        private @PlayerControls m_Wrapper;
        public CharacterControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_CharacterControls_Move;
        public InputAction @Run => m_Wrapper.m_CharacterControls_Run;
        public InputActionMap Get() { return m_Wrapper.m_CharacterControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControlsActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterControlsActions instance)
        {
            if (m_Wrapper.m_CharacterControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
            }
            m_Wrapper.m_CharacterControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
            }
        }
    }
    public CharacterControlsActions @CharacterControls => new CharacterControlsActions(this);

    // UserInterface
    private readonly InputActionMap m_UserInterface;
    private IUserInterfaceActions m_UserInterfaceActionsCallbackInterface;
    private readonly InputAction m_UserInterface_InventoryScreen;
    private readonly InputAction m_UserInterface_EquipmentScreen;
    private readonly InputAction m_UserInterface_AttributeScreen;
    public struct UserInterfaceActions
    {
        private @PlayerControls m_Wrapper;
        public UserInterfaceActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @InventoryScreen => m_Wrapper.m_UserInterface_InventoryScreen;
        public InputAction @EquipmentScreen => m_Wrapper.m_UserInterface_EquipmentScreen;
        public InputAction @AttributeScreen => m_Wrapper.m_UserInterface_AttributeScreen;
        public InputActionMap Get() { return m_Wrapper.m_UserInterface; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UserInterfaceActions set) { return set.Get(); }
        public void SetCallbacks(IUserInterfaceActions instance)
        {
            if (m_Wrapper.m_UserInterfaceActionsCallbackInterface != null)
            {
                @InventoryScreen.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnInventoryScreen;
                @InventoryScreen.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnInventoryScreen;
                @InventoryScreen.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnInventoryScreen;
                @EquipmentScreen.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnEquipmentScreen;
                @EquipmentScreen.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnEquipmentScreen;
                @EquipmentScreen.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnEquipmentScreen;
                @AttributeScreen.started -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnAttributeScreen;
                @AttributeScreen.performed -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnAttributeScreen;
                @AttributeScreen.canceled -= m_Wrapper.m_UserInterfaceActionsCallbackInterface.OnAttributeScreen;
            }
            m_Wrapper.m_UserInterfaceActionsCallbackInterface = instance;
            if (instance != null)
            {
                @InventoryScreen.started += instance.OnInventoryScreen;
                @InventoryScreen.performed += instance.OnInventoryScreen;
                @InventoryScreen.canceled += instance.OnInventoryScreen;
                @EquipmentScreen.started += instance.OnEquipmentScreen;
                @EquipmentScreen.performed += instance.OnEquipmentScreen;
                @EquipmentScreen.canceled += instance.OnEquipmentScreen;
                @AttributeScreen.started += instance.OnAttributeScreen;
                @AttributeScreen.performed += instance.OnAttributeScreen;
                @AttributeScreen.canceled += instance.OnAttributeScreen;
            }
        }
    }
    public UserInterfaceActions @UserInterface => new UserInterfaceActions(this);
    public interface ICharacterControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
    }
    public interface IUserInterfaceActions
    {
        void OnInventoryScreen(InputAction.CallbackContext context);
        void OnEquipmentScreen(InputAction.CallbackContext context);
        void OnAttributeScreen(InputAction.CallbackContext context);
    }
}
