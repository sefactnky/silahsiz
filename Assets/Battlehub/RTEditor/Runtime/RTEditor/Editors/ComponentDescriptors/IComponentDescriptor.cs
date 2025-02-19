﻿using Battlehub.RTCommon;
using Battlehub.Utils;
using System;
using System.Linq.Expressions;
using UnityEngine;

namespace Battlehub.RTEditor
{
    public struct HeaderDescriptor
    {
        public string DisplayName
        {
            get; set;
        }
        public bool ShowExpander
        {
            get; set;
        }

        public bool ShowResetButton
        {
            get; set;
        }

        public bool ShowEnableButton
        {
            get; set;
        }

        public bool ShowRemoveButton
        {
            get; set;
        }

        public Sprite Icon
        {
            get; set;
        }

        public bool ShowIcon
        {
            get;
            set;
        }

        public HeaderDescriptor(string displayName, bool showExpander = true, bool showResetButton = true, bool showEnableButton = true, bool showRemoveButton = true, bool showIcon = true, Sprite icon = null)
        {
            DisplayName = displayName;
            ShowExpander = showExpander;
            ShowResetButton = showResetButton;
            ShowEnableButton = showEnableButton;
            ShowRemoveButton = showRemoveButton;
            ShowIcon = showIcon;
            Icon = icon;
        }
    }

    public interface IComponentDescriptor
    {
        HeaderDescriptor GetHeaderDescriptor(IRTE editor);

        Type ComponentType { get; }

        Type GizmoType { get; }

        object CreateConverter(ComponentEditor editor);

        PropertyDescriptor[] GetProperties(ComponentEditor editor, object converter);
    }

    public class BuiltInDescriptorAttribute : Attribute
    {
    }

    public class ComponentDescriptorBase<TComponent> : IComponentDescriptor
    {
        public virtual HeaderDescriptor GetHeaderDescriptor(IRTE editor)
        {
            ILocalization localization = IOC.Resolve<ILocalization>();
            ISettingsComponent settingsComponent = IOC.Resolve<ISettingsComponent>();
            BuiltInWindowsSettings settings;
            if (settingsComponent == null)
            {
                settings = BuiltInWindowsSettings.Default;
            }
            else
            {
                settings = settingsComponent.BuiltInWindowsSettings;
            }

            Sprite icon = null;
            ThemeAsset selectedTheme = settingsComponent.SelectedTheme;
            if(selectedTheme != null)
            {
                icon = selectedTheme.GetIcon($"{ComponentType.Name} Icon");
            }

            var componentEditorSettings = settings.Inspector.ComponentEditor;
            return new HeaderDescriptor(
                localization.GetString("ID_RTEditor_CD_" + ComponentType.Name, ComponentType.Name),
                componentEditorSettings.ShowExpander,
                componentEditorSettings.ShowResetButton,
                componentEditorSettings.ShowEnableButton,
                componentEditorSettings.ShowRemoveButton,
                componentEditorSettings.ShowIcon,
                icon);
        }

        public virtual Type ComponentType
        {
            get { return typeof(TComponent); }
        }

        public virtual Type GizmoType
        {
            get { return null; }
        }

        public virtual object CreateConverter(ComponentEditor editor)
        {
            return null;
        }

        public virtual PropertyDescriptor[] GetProperties(ComponentEditor editor, object converter)
        {
            return IOC.Resolve<IEditorsMap>().GetDefaultPropertyDescriptors(ComponentType, editor);
        }

        protected PropertyDescriptor Property<T, U>(string name, Expression<Func<T, U>> expression, object propertMetadata = null)
        {
            return new PropertyDescriptor(name, Strong.MemberInfo(expression))
            {
                PropertyMetadata = propertMetadata,
            };
        }

        protected PropertyDescriptor Property<T, U>(string name, object[] targets, Expression<Func<T, U>> expression, object propertMetadata = null)
        {
            return new PropertyDescriptor(name, targets, Strong.MemberInfo(expression))
            {
                PropertyMetadata = propertMetadata,
            };
        }

        protected PropertyDescriptor Method<T>(string name, Expression<Func<T, Delegate>> expression)
        {
            return new PropertyDescriptor(name, Strong.MethodInfo(expression));
        }

        protected PropertyDescriptor Method<T>(string name, Expression<Action<T>> expression)
        {
            return new PropertyDescriptor(name, Strong.MethodInfo(expression));
        }

        protected PropertyDescriptor Method<T>(string name, object[] targets, Expression<Func<T, Delegate>> expression)
        {
            return new PropertyDescriptor(name, targets, Strong.MethodInfo(expression));
        }

        protected PropertyDescriptor Method<T>(string name, object[] targets, Expression<Action<T>> expression)
        {
            return new PropertyDescriptor(name, targets, Strong.MethodInfo(expression));
        }
    }

    public abstract class ComponentDescriptorBase<TComponent, TGizmo> : ComponentDescriptorBase<TComponent>
    {
        public override Type GizmoType
        {
            get { return typeof(TGizmo); }
        }
    }
}