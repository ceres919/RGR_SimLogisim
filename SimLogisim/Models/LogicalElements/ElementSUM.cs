using Avalonia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLogisim.Models.LogicalElements
{
    public class ElementSUM : ElementEntity
    {
        private Point startPoint;
        private ObservableCollection<int> enters;
        private ObservableCollection<int> exits;
        public override string Name { get; set; }

        public override ObservableCollection<int> Enters
        {
            get => enters;
            set
            {
                SetAndRaise(ref enters, value);
            }
        }
        public override ObservableCollection<int> Exits
        {
            get => exits;
            set => SetAndRaise(ref exits, value);
        }
        public override Point StartPoint
        {
            get => startPoint;
            set
            {
                Point oldPoint = StartPoint;

                SetAndRaise(ref startPoint, value);

                if (ChangeStartPoint != null)
                {
                    ChangeStartPointEventArgs args = new ChangeStartPointEventArgs
                    {
                        OldStartPoint = oldPoint,
                        NewStartPoint = StartPoint,
                    };

                    ChangeStartPoint(this, args);
                }
            }
        }
        public override void Logic()
        {
            int no_p;
            if (Enters[2] == 0)
                no_p = 1;
            else no_p = 0;
            Exits[0] = no_p & (Enters[0] | Enters[1] | Enters[2]) | (Enters[0] & Enters[1] & Enters[2]);
            Exits[1] = (Enters[0] & Enters[1]) | (Enters[0] & Enters[2]) | (Enters[1] & Enters[2]);
            if (ChangeStartPoint != null)
            {
                ChangeExitEventArgs args = new ChangeExitEventArgs
                {
                    Exits = this.Exits
                };
                ChangeExit(this, args);
            }
        }
        public override event EventHandler<ChangeExitEventArgs> ChangeExit;
        public override event EventHandler<ChangeStartPointEventArgs> ChangeStartPoint;
    }
}
