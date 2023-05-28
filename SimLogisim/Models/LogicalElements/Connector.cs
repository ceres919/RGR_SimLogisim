using Avalonia;
using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLogisim.Models.LogicalElements
{
    public class Connector : IShape, IDisposable
    {
        private Point startPoint;
        private Point endPoint;
        private int enterIndex, exitIndex;
        private string enterRectangle;
        private ElementEntity firstRectangle;
        private ElementEntity secondRectangle;
        public string Name { get; set; }
        public Point StartPoint
        {
            get => startPoint;
            set => SetAndRaise(ref startPoint, value);
        }

        public Point EndPoint
        {
            get => endPoint;
            set => SetAndRaise(ref endPoint, value);
        }
        public int EnterIndex
        {
            get => enterIndex;
            set => SetAndRaise(ref enterIndex, value);
        }
        public int ExitIndex
        {
            get => exitIndex;
            set => SetAndRaise(ref exitIndex, value);
        }
        public string EnterRectangle
        {
            get => enterRectangle;
            set => SetAndRaise(ref enterRectangle, value);
        }
        public ElementEntity FirstRectangle
        {
            get => firstRectangle;
            set
            {
                if (firstRectangle != null)
                {
                    firstRectangle.ChangeStartPoint -= OnFirstRectanglePositionChanged;
                    firstRectangle.ChangeExit -= OnFirstRectangleExitChanged;
                }

                firstRectangle = value;

                if (firstRectangle != null)
                {
                    firstRectangle.ChangeStartPoint += OnFirstRectanglePositionChanged;
                    firstRectangle.ChangeExit += OnFirstRectangleExitChanged;
                }
            }
        }

        public ElementEntity SecondRectangle
        {
            get => secondRectangle;
            set
            {
                if (secondRectangle != null)
                {
                    secondRectangle.ChangeStartPoint -= OnSecondRectanglePositionChanged;
                    secondRectangle.ChangeExit -= OnSecondRectangleExitChanged;
                }

                secondRectangle = value;

                if (secondRectangle != null)
                {
                    secondRectangle.ChangeStartPoint += OnSecondRectanglePositionChanged;
                    secondRectangle.ChangeExit += OnSecondRectangleExitChanged;
                }
            }
        }

        private void OnFirstRectanglePositionChanged(object? sender, ChangeStartPointEventArgs e)
        {
            StartPoint += e.NewStartPoint - e.OldStartPoint;
        }

        private void OnSecondRectanglePositionChanged(object? sender, ChangeStartPointEventArgs e)
        {
            EndPoint += e.NewStartPoint - e.OldStartPoint;
        }
        private void OnFirstRectangleExitChanged(object? sender, ChangeExitEventArgs e)
        {
            if(EnterRectangle == "second")
            {
                SecondRectangle.Enters[EnterIndex] = e.Exits[ExitIndex];
                SecondRectangle.Logic();
            }
        }

        private void OnSecondRectangleExitChanged(object? sender, ChangeExitEventArgs e)
        {
            if(enterRectangle == "first")
            {
                FirstRectangle.Enters[EnterIndex] = e.Exits[ExitIndex];
                FirstRectangle.Logic();
            }
        }
        public void Dispose()
        {
            if (FirstRectangle != null)
            {
                FirstRectangle.ChangeStartPoint -= OnFirstRectanglePositionChanged;
                FirstRectangle.ChangeExit -= OnFirstRectangleExitChanged;
            }

            if (SecondRectangle != null)
            {
                SecondRectangle.ChangeStartPoint -= OnSecondRectanglePositionChanged;
                SecondRectangle.ChangeExit -= OnSecondRectangleExitChanged;

            }
        }
    }
}
