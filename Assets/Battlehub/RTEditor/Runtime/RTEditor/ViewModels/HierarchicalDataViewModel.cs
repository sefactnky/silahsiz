﻿using Battlehub.RTCommon;
using Battlehub.RTEditor.Models;
using Battlehub.UIControls.Binding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityWeld.Binding;

namespace Battlehub.RTEditor.ViewModels
{
    public class ContextMenuEventArgs : ContextMenuArgs
    {
        private List<MenuItemViewModel> m_menuItems;

        public override IReadOnlyList<ContextMenuItem> Items
        {
            get { return m_menuItems; }
        }

        private string m_windowName;
        public override string WindowName
        {
            get { return m_windowName; }
        }

        private ContextMenuAnchor m_anchor;
        public override ContextMenuAnchor Anchor
        {
            get { return m_anchor; }
        }

        public ContextMenuEventArgs(string windowName, List<MenuItemViewModel> menuItems)
        {
            m_windowName = windowName;
            m_menuItems = menuItems;
        }

        public ContextMenuEventArgs(string viewName, ContextMenuAnchor anchor,  List<MenuItemViewModel> menuItems)
        {
            m_windowName = viewName;
            m_menuItems = menuItems;

            m_anchor = anchor;
        }

        public override ContextMenuItem AddMenuItem(string path, Action<string> action, Action<ContextMenuItem.ValidationArgs> validate = null)
        {
            var menuItem = new MenuItemViewModel
            {
                Path = path,
                Action = action,
                Validate = validate
            };
            m_menuItems.Add(menuItem);
            return menuItem;
        }

        public override ContextMenuItem InsertMenuItem(int index, string path, Action<string> action, Action<ContextMenuItem.ValidationArgs> validate = null)
        {
            var menuItem = new MenuItemViewModel
            {
                Path = path,
                Action = action,
                Validate = validate
            };
            m_menuItems.Insert(index, menuItem);
            return menuItem;
        }

        public override void RemoveMenuItem(int index)
        {
            m_menuItems.RemoveAt(index);
        }

        public override void ClearMenuItems()
        {
            m_menuItems.Clear();
        }

        [Obsolete]
        public List<MenuItemViewModel> MenuItems
        {
            get { return m_menuItems; }
        }
    }

    [Binding]
    public class HierarchicalDataViewModel<T> : ViewModel, IHierarchicalData<T> where T: class
    {
        public event EventHandler<HierarchicalDataChangedEventArgs> HierarchicalDataChanged;
        protected void RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs args)
        {
            HierarchicalDataChanged?.Invoke(this, args);
        }

        protected void RaiseItemAdded(object parentItem, object item)
        {
            RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.ItemAdded(parentItem, item));
        }

        protected void RaiseItemInserted(int index, object item)
        {
            RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.ItemInserted(index, item));
        }

        protected void RaiseItemRemoved(object parentItem, object item)
        {
            RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.ItemRemoved(parentItem, item));
        }

        protected void RaiseRemoveSelected()
        {
            RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.RemoveSelected());
        }

        protected void RaiseNextSiblingChanged(object nextSibling, object item)
        {
            RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.NextSiblingsChanged(nextSibling, item));
        }

        protected void RaisePrevSiblingChanged(object prevSibling, object item)
        {
            RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.PrevSiblingsChanged(prevSibling, item));
        }

        protected void RaiseParentChanged(object oldParent, object item)
        {
            RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.ParentChanged(oldParent, item));
        }
        protected void RaiseExpand( object item)
        {
            RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.Expand(item));
        }

        protected void RaiseCollapse(object item)
        {
            RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.Collapse(item));
        }

        protected void RaiseSelect(IEnumerable items, object scrollIntoView = null)
        {
            RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.Select(items, scrollIntoView));
        }

        protected void RaiseReset(object item = null)
        {
            RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.Reset(item));
        }

        protected void RaiseDataBindVisible()
        {
            RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.DataBindVisible());
        }

        [Binding]
        public IHierarchicalData<T> DataSource
        {
            get { return this; }
        }

        public virtual void BindData()
        {
            m_expandedItems = new HashSet<T>();

            RaisePropertyChanged(nameof(DataSource));
        }

        public bool HasSelectedItems
        {
            get { return m_selectedItems != null && m_selectedItems.Any(); }
        }

        public virtual T SelectedItem
        {
            get { return m_selectedItems != null ? m_selectedItems.FirstOrDefault() : default; }
            set
            { 
                if(value != null)
                {
                    SelectedItems = new[] { value };
                }
                else
                {
                    SelectedItems = null;
                }
            }
        }

        private bool m_scrollIntoView;
        public bool ScrollIntoView
        {
            get { return m_scrollIntoView; }
            set { m_scrollIntoView = value; }
        }

        protected IEnumerable<T> m_selectedItems;
        public virtual IEnumerable<T> SelectedItems
        {
            get { return m_selectedItems; }
            set
            {
                if (m_selectedItems != value)
                {
                    IEnumerable<T> unselectedObjects = m_selectedItems;
                    m_selectedItems = value;
                    OnSelectedItemsChanged(unselectedObjects, m_selectedItems);
                    if(ScrollIntoView && m_selectedItems != null && m_selectedItems.Any())
                    {
                        RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.Select(m_selectedItems, m_selectedItems.First()));
                    }
                    else
                    {
                        RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.Select(m_selectedItems));
                    }
                }
            }
        }
        protected virtual void OnSelectedItemsChanged(IEnumerable<T> unselectedObjects, IEnumerable<T> selectedObjects)
        {
        }

        private bool m_isEditing;
        [Binding]
        public bool IsEditing
        {
            get { return m_isEditing; }
            set
            {
                if (m_isEditing != value)
                {
                    m_isEditing = value;
                    RaisePropertyChanged(nameof(IsEditing));
                    m_isEditing = false;
                }
            }
        }

        private HashSet<T> m_expandedItems;
        public bool IsExpanded(T obj)
        {
            if (m_expandedItems == null)
            {
                return false;
            }

            if(obj == null)
            {
                return true;
            }

            return m_expandedItems.Contains(obj);
        }

        protected enum DragDropAction
        {
            None,
            SetLastChild,
            SetNextSibling,
            SetPrevSilbling,
        }

        private DragDropAction m_lastDragDropAction;
        protected DragDropAction LastDragDropAction
        {
            get { return m_lastDragDropAction; }
            set { m_lastDragDropAction = value; }
        }

        private bool m_canDropItems;
        [Binding]
        public bool CanDropItems
        {
            get { return m_canDropItems; }
            set
            {
                if (m_canDropItems != value)
                {
                    m_canDropItems = value;
                    RaisePropertyChanged(nameof(CanDropItems));
                }
            }
        }

        private IEnumerable<T> m_sourceItems;
        [Binding]
        public IEnumerable<T> SourceItems
        {
            get { return m_sourceItems; }
            set { m_sourceItems = value; }
        }

        private T m_targetItem;
        [Binding]
        public virtual T TargetItem
        {
            get { return m_targetItem; }
            set
            {
                if (!Equals(m_targetItem, value))
                {
                    m_targetItem = value;

                    if (ExternalDragObjects != null)
                    {
                        CanDropExternalObjects = AllowDropExternalObjects();
                        RaisePropertyChanged(nameof(CanDropExternalObjects));
                    }
                    else if (SourceItems != null)
                    {
                        CanDropItems = m_lastDragDropAction != DragDropAction.None && m_targetItem != null;
                    }
                }
            }
        }

        #region Context Menu
        public event EventHandler<ContextMenuEventArgs> ContextMenuOpened;
        protected void RaiseContextMenuOpened(ContextMenuEventArgs args)
        {
            ContextMenuOpened?.Invoke(this, args);
        }

        private IEnumerable<MenuItemViewModel> m_contextMenu;
        [Binding]
        public IEnumerable<MenuItemViewModel> ContextMenu
        {
            get { return m_contextMenu; }
            set
            {
                if (m_contextMenu != value)
                {
                    m_contextMenu = value;
                    RaisePropertyChanged(nameof(ContextMenu));
                }
            }
        }

        protected virtual ContextMenuAnchor GetContextMenuAnchor()
        {
            return new ContextMenuAnchor(TargetItem, SelectedItems?.ToArray());
        }

        protected virtual void OpenContextMenu()
        {
            List<MenuItemViewModel> menuItems = new List<MenuItemViewModel>();
            OnContextMenu(menuItems);

            var args = new ContextMenuEventArgs(WindowName, GetContextMenuAnchor(), menuItems);
            
            var contextMenuModel = IOC.Resolve<IContextMenuModel>();
            contextMenuModel?.RaiseOpen(args);

            ContextMenuOpened?.Invoke(this, args);
            ContextMenu = menuItems;
        }

        protected virtual void OnContextMenu(List<MenuItemViewModel> menuItems)
        {

        }

        #endregion

        protected override void Awake()
        {
            base.Awake();
            m_expandedItems = new HashSet<T>();
        }

        protected override void OnDestroy()
        {
            m_expandedItems = null;
            base.OnDestroy();
        }

        #region IHierarchicalData
        
        public virtual IEnumerable<T> GetChildren(T parent)
        {
            return new T[0];
        }

        public virtual HierarchicalDataFlags GetFlags()
        {
            return HierarchicalDataFlags.CanUnselectAll;
        }        

        public virtual HierarchicalDataItemFlags GetItemFlags(T item)
        {
            return HierarchicalDataItemFlags.None;
        }

        public virtual T GetParent(T item)
        {
            return default;
        }

        public virtual bool HasChildren(T parent)
        {
            return false;
        }

        public virtual int IndexOf(T parent, T item)
        {
            throw new NotSupportedException();
        }


        public virtual void Expand(T item)
        {
            T parent = GetParent(item);
            if(parent != null && !IsExpanded(parent))
            {
                Expand(parent);
            }

            if(HasChildren(item))
            {
                m_expandedItems.Add(item);
            }
            RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.Expand(item));
        }

        
        public virtual void Collapse(T item)
        {
            m_expandedItems.Remove(item);
            RaiseHierarchicalDataChanged(HierarchicalDataChangedEventArgs.Collapse(item));
        }

        public virtual void Select(IEnumerable<T> items)
        {
            SelectedItems = items;
        }

        #endregion

        #region Bound UnityEvent Handlers
        [Binding]
        public virtual void OnItemsBeginDrag()
        {
            m_lastDragDropAction = DragDropAction.None;
        }

        [Binding]
        public virtual void OnItemDragEnter()
        {

        }

        [Binding]
        public virtual void OnItemDragLeave()
        {
            if (ExternalDragObjects == null)
            {
                CanDropItems = false;
            }
        }

        [Binding]
        public virtual void OnItemsDrag()
        {
        }

        [Binding]
        public virtual void OnItemsSetLastChild()
        {
            m_lastDragDropAction = DragDropAction.SetLastChild;

            if (ExternalDragObjects == null)
            {
                CanDropItems = true;
            }
        }

        [Binding]
        public virtual void OnItemsSetNextSibling()
        {
            m_lastDragDropAction = DragDropAction.SetNextSibling;
            if (ExternalDragObjects == null)
            {
                CanDropItems = true;
            }
        }

        [Binding]
        public virtual void OnItemsSetPrevSibling()
        {
            m_lastDragDropAction = DragDropAction.SetPrevSilbling;
            if (ExternalDragObjects == null)
            {
                CanDropItems = true;
            }
        }

        [Binding]
        public virtual void OnItemsCancelDrop()
        {
            m_lastDragDropAction = DragDropAction.None;
            if (ExternalDragObjects == null)
            {
                CanDropItems = false;
            }
        }
      

        [Binding]
        public virtual void OnItemsBeginDrop()
        {   
        }

        [Binding]
        public virtual void OnItemsDrop()
        {
            if (ExternalDragObjects == null)
            {
                CanDropItems = false;
            }
        }

        [Binding]
        public virtual void OnItemsEndDrag()
        {
            if (ExternalDragObjects == null)
            {
                CanDropItems = false;
            }
        }

        [Binding]
        public virtual void OnItemsRemoved()
        {
        }

        [Binding]
        public virtual void OnItemBeginEdit()
        {
        }

        [Binding]
        public virtual void OnItemEndEdit()
        {
        }

        [Binding]
        public virtual void OnItemHold()
        {
           // OpenContextMenu();
        }

        [Binding]
        public virtual void OnItemClick()
        {
            OpenContextMenu();
        }

        [Binding]
        public virtual void OnItemDoubleClick()
        {
        }

        [Binding]
        public virtual void OnHold()
        {
            OpenContextMenu();
        }

        [Binding]
        public virtual void OnClick()
        {
            OpenContextMenu();
        }

        public override void OnExternalObjectEnter()
        {
            LastDragDropAction = DragDropAction.None;
            CanDropExternalObjects = AllowDropExternalObjects();
        }

        public override void OnExternalObjectLeave()
        {
            CanDropExternalObjects = false;
        }

        #endregion

        #region Methods
        protected virtual bool AllowDropExternalObjects()
        {
            return false;
        }

        protected virtual void ExpandTo(T item)
        {
            if (item == null)
            {
                return;
            }

            T parent = GetParent(item);
            if (parent != null && !IsExpanded(parent))
            {
                ExpandTo(parent);
                Expand(parent);
            }
        }

        protected virtual void ExpandAll(T item)
        {
            if (HasChildren(item))
            {
                Expand(item);

                foreach (T child in GetChildren(item))
                {
                    ExpandAll(child);
                }
            }
        }

        protected virtual void GetExpandedItems(IEnumerable<T> objects, List<T> result)
        {
            foreach (T obj in objects)
            {
                result.Add(obj);
                if (IsExpanded(obj) && HasChildren(obj))
                {
                    GetExpandedItems(GetChildren(obj), result);
                }
            }
        }

        protected List<T> GetExpandedItems()
        {
            List<T> expandedItems = new List<T>();
            GetExpandedItems(GetChildren(default), expandedItems);
            return expandedItems;
        }

        #endregion

        #region Legacy

        //[Obsolete]
        public virtual void Add(T parent, T item)
        {

        }

        //[Obsolete]
        public virtual void Insert(T parent, T item, int index)
        {

        }

        //[Obsolete]
        public virtual void Remove(T parent, T item)
        {

        }

        #endregion

    }
}

