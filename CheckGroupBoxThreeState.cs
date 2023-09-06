using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace checkbox
{
    internal class CheckGroupBoxThreeState : GroupBox
    {
        CheckBox mainCB;
        List<CheckBox> checkBoxes;

        public CheckGroupBoxThreeState() : base()
        {
            checkBoxes = new List<CheckBox>();
            mainCB = new CheckBox();
            mainCB.CheckStateChanged += mainCB_CheckedChanged;
            Controls.Add(mainCB);

            ControlAdded += thisControlAdded;
        }

        private void ModifyMain(object sender, EventArgs e)
        {
            bool checkedPresent = false, uncheckedPresent = false;
            foreach (CheckBox checkBox in checkBoxes)
            {
                if (checkBox.Checked)
                {
                    checkedPresent = true;
                }
                else if (!checkBox.Checked)
                {
                    uncheckedPresent = true;
                }
            }
            if (checkedPresent && !uncheckedPresent)
            {
                mainCB.CheckState = CheckState.Checked;
            }
            else if (uncheckedPresent && !checkedPresent)
            {
                mainCB.CheckState = CheckState.Unchecked;
            }
            else
            {
                mainCB.CheckState = CheckState.Indeterminate;
            }
        }

        private void thisControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control.GetType() == typeof(CheckBox))
            {
                (e.Control as CheckBox).CheckedChanged += ModifyMain;
                checkBoxes.Add(e.Control as CheckBox);
            }
        }

        private void mainCB_CheckedChanged(object sender, EventArgs e)
        {
            switch (mainCB.CheckState)
            {
                case CheckState.Checked:
                    {
                        foreach (CheckBox cb in checkBoxes)
                        {
                            cb.Checked = true;
                        }
                        break;
                    }
                case CheckState.Unchecked:
                    {
                        foreach (CheckBox cb in checkBoxes)
                        {
                            cb.Checked = false;
                        }
                        break;
                    }
            }
        }
    }
}
