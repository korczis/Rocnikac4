using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace rocnikacV4
{
    public class UndoRedo
    {
        #region Pomocné proměnné
        private Friska_dama mainDialog;
        private Button undoButton;
        private Button redoButton;
        private ListBox history;
        #endregion

        #region Konstruktor
        public UndoRedo(Friska_dama mDialog)
        {
            this.mainDialog = mDialog;
            this.undoButton = mDialog.UndoButton;
            this.redoButton = mDialog.RedoButton;
            this.history = mDialog.History;
        }
        #endregion

    }
}
