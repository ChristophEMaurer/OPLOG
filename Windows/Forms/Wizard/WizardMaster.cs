using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace Windows.Forms.Wizard
{
    public abstract partial class WizardMaster : Form
    {
        private const string FormName = "WizardMaster";
        abstract protected string GetText(string formName, string id);

        protected bool isUpdate = false;

        private bool _allDone = false;

        private const int LABEL_HEIGHT = 30;

        private bool __bAnimate = false;

        private bool _formMayClose = true;

        public enum AnimateStyles
        {
            Slide = 262144,
            Activate = 131072,
            Blend = 524288,
            Hide = 65536,
            Center = 16,
            HOR_Positive = 1,
            HOR_Negative = 2,
            VER_Positive = 4,
            VER_Negative = 8
        }
        private List<WizardPage> _oPages;
        private List<Label> _oLabels;

        private WizardPage _oCurrentPage = null;
        private int _nIndexCurrentPage;

        public bool UpdateMode
        {
            get { return isUpdate; }
        }

        public WizardMaster() :
            this(null)
        {
        }

        public WizardMaster(string[] args)
        {
            if (args != null && args.Length > 0 && args[0] == "update")
            {
                isUpdate = true;
            }

            _oPages = new List<WizardPage>();

            InitializeComponent();

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void CreateLabels()
        {
            _oLabels = new List<Label>();

            pnlLeft.SuspendLayout();

            for (int i = 0; i < _oPages.Count; i++)
            {
                WizardPage page = _oPages[i];

                page.Width = picPlaceholder.Width;
                page.Height = picPlaceholder.Height;

                Label lbl = new Label();
                _oLabels.Add(lbl);

                lbl.Left = pnlLeft.Left;
                lbl.Width = pnlLeft.Width;
                lbl.Height = LABEL_HEIGHT;
                lbl.Top = i * LABEL_HEIGHT;
                lbl.Text = (i + 1).ToString() + ". " + page.PageName;
                lbl.TextAlign = ContentAlignment.MiddleLeft;

                pnlLeft.Controls.Add(lbl);
            }
            pnlLeft.ResumeLayout();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

#if DEBUG
            this.Text = this.GetType().FullName + "-" + Title;
#else
            this.Text = Title;
#endif
            this.cmdFinish.Text = FinishText;

            CreateLabels();
            ActivatePage(0);
        }

        public virtual string FinishText
        {
            get { return GetText(FormName, "fertigstellen"); }
        }

        public virtual string CloseText
        {
            get { return GetText(FormName, "schliessen"); }
        }

        /// <summary>
        /// Bei 1 bis N Seiten:
        /// 1 bis N - 2 sind irgendwelche Seiten
        /// N-1 ist die Action Seite
        /// N ist die Info Seite: hat geklappt oder auch nicht
        /// </summary>
        /// <param name="nIndex"></param>
        private void EnableControls(int nIndex)
        {
            if (nIndex > 0 && nIndex < _oPages.Count - 1)
            {
                cmdBack.Enabled = true;
            }
            else
            {
                cmdBack.Enabled = false;
            }

            if (nIndex == _oPages.Count - 2)
            {
                // Vorletzte Seite ist immer die Action-Seite
                AcceptButton = cmdFinish;
                cmdNext.Enabled = false;
                cmdNext.Visible = false;
                cmdFinish.Enabled = true;
                cmdFinish.Visible = true;
                cmdFinish.Focus();
            }
            else
            {
                if (nIndex == _oPages.Count - 1)
                {
                    // letzte Seite
                    AcceptButton = cmdFinish;
                    cmdCancel.Enabled = false;
                    cmdBack.Enabled = false;
                    cmdNext.Enabled = false;
                    cmdNext.Visible = false;
                    //cmdFinish.Text = CloseText;
                    cmdFinish.Text = CloseText;
                    cmdFinish.Enabled = true;
                    cmdFinish.Visible = true;
                    cmdFinish.Focus();
                }
                else
                {
                    // erste bis vorletzte Seite
                    AcceptButton = cmdNext;
                    cmdNext.Enabled = true;
                    cmdNext.Visible = true;
                    cmdFinish.Enabled = false;
                    cmdFinish.Visible = false;
                }
            }
        }

        private void ActivatePage(int nIndexNewPage)
        {
            if (nIndexNewPage >= 0 && nIndexNewPage < _oPages.Count)
            {
                // alte Seite holen, text ändern usw.
                if (_oCurrentPage != null)
                {
                    _oLabels[_nIndexCurrentPage].ForeColor = Color.Black;
                    //_oLabels[_nIndexCurrentPage].BackColor = pnlLeft.BackColor;
                    //_oLabels[_nIndexCurrentPage].Font = new Font(_oLabels[_nIndexCurrentPage].Font, FontStyle.Regular);
                    _oCurrentPage.OnDeactivate();

                    if (__bAnimate)
                    {
                        Win32Interop.AnimateWindow((uint)this.Handle.ToInt32(), (uint)200, (uint)(AnimateStyles.Blend | AnimateStyles.Hide));
                    }
                }

                // Neue Seite
                WizardPage newPage = _oPages[nIndexNewPage];
                newPage.OnActivate();

                //_oLabels[nIndexNewPage].Font = new Font(_oLabels[_nIndexCurrentPage].Font, FontStyle.Bold);
                _oLabels[nIndexNewPage].ForeColor = Color.Blue;
                //_oLabels[nIndexNewPage].BackColor = BackColor;

                picImage.Image = newPage.Image;
                picImage.SizeMode = PictureBoxSizeMode.Zoom;

                if (_oCurrentPage != null)
                {
                    _oCurrentPage.Visible = false;
                    _oCurrentPage.Enabled = false;
                }

                picPlaceholder.Controls.Clear();
                picPlaceholder.Controls.Add(newPage);

                EnableControls(nIndexNewPage);

                lblHeader1.Text = newPage.Header1;
                lblHeader2.Text = newPage.Header2;

                if (__bAnimate)
                {
                    Win32Interop.AnimateWindow((uint)this.Handle.ToInt32(), (uint)200, (uint)AnimateStyles.Blend);
                }

                newPage.Enabled = true;
                newPage.Visible = true;

                _oCurrentPage = newPage;
                _nIndexCurrentPage = nIndexNewPage;

                newPage.SetInitialFocus();

            }
        }

/*
        private string PageNameInOverview(WizardPage page)
        {
            return PageNameInOverview(page, false);
        }

        private string PageNameInOverview(WizardPage page, bool bSelected)
        {
            string strName;

            if (bSelected)
            {
                strName = ">> " + page.PageName;
            }
            else
            {
                strName = page.PageName;
            }

            return strName;
        }
*/

        public void AddPage(WizardPage page)
        {
            page.Wizard = this;
            page.Enabled = false;
            page.Visible = false;
            page.BorderStyle = BorderStyle.None;
            page.Left = 0;
            page.Top = 0;
            page.Size = picPlaceholder.Size;

            _oPages.Add(page);
            page.PageIndex = _oPages.Count;
        }

        public void PrevPage()
        {
            WizardPage page = _oPages[_nIndexCurrentPage];

            if (page.OnPreBack())
            {
                ActivatePage(_nIndexCurrentPage - 1);
            }
        }
        public void NextPage()
        {
            WizardPage page = _oPages[_nIndexCurrentPage];

            if (page.OnPreNext())
            {
                ActivatePage(_nIndexCurrentPage + 1);
            }
        }

        private void Finish()
        {
            if (_allDone)
            {
                if (_nIndexCurrentPage == _oPages.Count - 1)
                {
                    OnPreClose();
                    Close();
                }
                else
                {
                    NextPage();
                }
            }
            else
            {
                WizardPage page = _oPages[_nIndexCurrentPage];

                if (page.OnPreNext() && OnPreFinish())
                {
                    bool success = page.OnFinish();

                    success = success && OnFinish();

                    if (OnPostFinish(success))
                    {
                        _allDone = true;

                        if (_nIndexCurrentPage == _oPages.Count - 1)
                        {
                            AcceptButton = cmdFinish;
                            cmdBack.Enabled = false;
                            cmdNext.Enabled = false;
                            cmdNext.Visible = false;
                            cmdCancel.Enabled = false;
                            cmdFinish.Text = GetText(FormName, "schliessen");
                        }
                        else
                        {
                            NextPage();
                        }
                    }
                }
            }
        }

        private void Cancel()
        {
            if (OnPreCancel())
            {
                OnCancel();
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            NextPage();
        }

        private void cmdFinish_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            PrevPage();
        }

        /// <summary>
        /// Override this method to set the window title of this Wizard
        /// </summary>
        public virtual string Title 
        {
            get { return GetText(FormName, "assistent"); } 
        }

        protected virtual void OnPreClose() { }
        protected virtual bool OnPreFinish() { return true; }
        protected virtual bool OnFinish() { return true; }
        protected virtual bool OnPostFinish(bool bOnFinish) { return true; }
        protected virtual bool OnPreCancel() { return true; }
        protected virtual void OnCancel() 
        {
            this.DialogResult = DialogResult.Cancel;
            Close(); 
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void WizardMaster_Load(object sender, EventArgs e)
        {
            // MAn soll im Designer beide Buttons sehen, 
            // zur Laufzeit sollen Sie uebereinander liegen.
            cmdFinish.Left = cmdNext.Left;
        }
        public void DisableAll()
        {
            cmdBack.Enabled = false;
            cmdCancel.Enabled = false;
            cmdNext.Enabled = false;
            cmdFinish.Enabled = false;

            //
            // Wenn alle cmd disabled sind und alle anderen controls readonly sind, bekommt eine 
            // readonly textbox den Fokus und alles darin wird selektiert.
            // Verhindern, dass der gesamte Text von readonly text boxen selektriert (blau) wird:
            // Die erste Textbox suchen, und für diese select(0, 0) aufrufen.
            //
            foreach (Control control in Controls)
            {
                if (FixSelection(control))
                {
                    break;
                }
            }
        }

        private bool FixSelection(Control control)
        {
            bool done = false;

            if (control.GetType() == typeof(TextBox))
            {
                TextBox txt = (TextBox)control;
                txt.Select(0, 0);
                done = true;
            }
            else
            {
                foreach (Control c in control.Controls)
                {
                    if (FixSelection(c))
                    {
                        break;
                    }
                }
            }

            return done;
        }

        public void FormMayClose(bool formMayClose)
        {
            _formMayClose = formMayClose;
        }
        private void WizardMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_formMayClose)
            {
                e.Cancel = true;
            }
        }
    }
}
