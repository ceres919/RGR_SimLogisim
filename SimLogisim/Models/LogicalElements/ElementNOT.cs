﻿using Avalonia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLogisim.Models.LogicalElements
{
    public class ElementNOT : ElementEntity
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
            if(Enters[0] == 0)
                Exits[0] = 1;
            else Exits[0] = 0;
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
