﻿using System;

namespace Battlehub.RTEditor
{
    [Obsolete]
    public class ProjectTreeViewInput : BaseViewInput<ProjectTreeView>
    {
        protected override void UpdateOverride()
        {
            base.UpdateOverride();

            if(DeleteAction())
            {
                View.DeleteSelectedItems();
            }

            if (SelectAllAction())
            {
                View.SelectAll();
            }
        }
    }
}
