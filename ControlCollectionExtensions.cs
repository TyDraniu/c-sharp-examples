using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Controls
    {
    /// <summary>
    ///
    /// </summary>
    public static class ControlCollectionExtensions
        {
        /// <summary>
        /// Znajduje kontrolki danego typu
        /// </summary>
        /// <param name="ControlCollection"></param>
        /// <param name="SearchAllChildren"></param>
        /// <param name="allowedTypes"></param>
        /// <returns></returns>
        public static List<Control> FindControlsByType(this Control.ControlCollection ControlCollection, bool SearchAllChildren, params Type[] allowedTypes)
            {
            List<Control> lstControls = new List<Control>();
            ControlCollection.ForEach(ctrl =>
            {
                if ((SearchAllChildren) && (ctrl is CPanel || ctrl is CGroupBox || ctrl is CTabControl || ctrl is TabPage))
                    {
                    lstControls.AddRange(ctrl.Controls.FindControlsByType(true, allowedTypes));
                    }
                if (new List<Type>(allowedTypes).Contains(ctrl.GetType()))
                    lstControls.Add(ctrl);
            });

            return lstControls;
            }

        /// <summary>
        /// Wykonuje akcję dla wszystkich kontrolek w tej kolekcji
        /// </summary>
        /// <param name="ControlCollection"></param>
        /// <param name="Action"></param>
        public static void ForEach(this Control.ControlCollection ControlCollection, Action<Control> Action)
            {
            foreach (Control c in ControlCollection)
                {
                if (c is CPanel || c is CGroupBox || c is CTabControl || c is TabPage)
                    {
                    c.Controls.ForEach(Action);
                    }
                else
                    {
                    Action.Invoke(c);
                    }
                }
            }

        /// <summary>
        /// Wykonuje akcję dla wszystkich kontrolek w tej grupie
        /// </summary>
        /// <param name="GroupBox"></param>
        /// <param name="Action"></param>
        public static void ForEachControl(this CGroupBox GroupBox, Action<Control> Action)
            {
            foreach (Control c in GroupBox.Controls)
                {
                if (c is CPanel || c is CGroupBox || c is CTabControl || c is TabPage)
                    {
                    c.Controls.ForEach(Action);
                    }
                else
                    {
                    Action.Invoke(c);
                    }
                }
            }

        /// <summary>
        /// Wykonuje akcję dla wszystkich kontrolek danego typu w tej kolekcji
        /// </summary>
        /// <param name="ControlCollection"></param>
        /// <param name="Action"></param>
        /// <param name="allowedTypes"></param>
        public static void ForEachOfType(this Control.ControlCollection ControlCollection, Action<Control> Action, params Type[] allowedTypes)
            {
            foreach (Control c in ControlCollection)
                {
                if (c is CPanel || c is CGroupBox || c is CTabControl || c is TabPage)
                    {
                    c.Controls.ForEachOfType(Action, allowedTypes);
                    }
                else if (new List<Type>(allowedTypes).Contains(c.GetType()))
                    {
                    Action.Invoke(c);
                    }
                }
            }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ControlCollection"></param>
        /// <param name="IncludeChildren"></param>
        /// <returns></returns>
        public static List<Control> ToList(this Control.ControlCollection ControlCollection, bool IncludeChildren)
            {
            List<Control> lstControls = new List<Control>();
            ControlCollection.ForEach(ctrl =>
            {
                if ((IncludeChildren) && (ctrl is CPanel || ctrl is CGroupBox || ctrl is CTabControl || ctrl is TabPage))
                    lstControls.AddRange(ctrl.Controls.ToList(true));
                lstControls.Add(ctrl);
            });
            return lstControls;
            }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ControlCollection"></param>
        /// <returns></returns>
        public static List<Control> ToList(this Control.ControlCollection ControlCollection)
            {
            List<Control> lstControls = new List<Control>();
            ControlCollection.ForEach(ctrl =>
            {
                if (!(ctrl is CPanel || ctrl is CGroupBox || ctrl is CTabControl || ctrl is TabPage))
                    lstControls.AddRange(ctrl.Controls.ToList(true));
                lstControls.Add(ctrl);
            });
            return lstControls;
            }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ControlCollection"></param>
        /// <param name="Condition"></param>
        /// <param name="SearchAllChildren"></param>
        /// <returns></returns>
        public static List<Control> Where(this Control.ControlCollection ControlCollection, Func<Control, bool> Condition, bool SearchAllChildren)
            {
            List<Control> lstControls = new List<Control>();
            ControlCollection.ForEach(ctrl =>
            {
                if ((SearchAllChildren) && (ctrl is CPanel || ctrl is CGroupBox || ctrl is CTabControl || ctrl is TabPage))
                    {
                    lstControls.AddRange(ctrl.Controls.Where(Condition, true));
                    }
                if (Condition.Invoke(ctrl))
                    lstControls.Add(ctrl);
            });
            return lstControls;
            }
        }
    }