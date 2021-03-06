using System;
using System.Collections.Generic;
using LibUISharp.Drawing;

namespace LibUISharp.Demos.Histogram
{
    public class HistogramWindow : Window
    {
        private StackPanel hPanel = new StackPanel(Orientation.Horizontal) { Padding = true };
        private StackPanel vPanel = new StackPanel(Orientation.Vertical) { Padding = true };
        private List<SpinBox> spinBoxList = new List<SpinBox>();
        private ColorPicker colorPicker = new ColorPicker() { Color = Colors.Blue };
        private Surface histogramSurface;

        public HistogramWindow() : base(640, 480, "LibUISharp Histogram Demo", true) => InitializeComponent();

        protected override void InitializeComponent()
        {
            Margins = true;
            Child = hPanel;
            
            hPanel.Children.Add(vPanel);
            
            for (int i = 0; i < 10; i++)
            {
                SpinBox spinBox = new SpinBox(0, 100) { Value = new Random(DateTime.Now.Millisecond).Next(0, 101) };
                spinBox.ValueChanged += SpinBoxOnValueChanged;
                vPanel.Children.Add(spinBox);
                spinBoxList.Add(spinBox);
            }
            
            colorPicker.ColorChanged += (sender, args) => { histogramSurface.QueueRedrawAll(); };

            vPanel.Children.Add(colorPicker);

            histogramSurface = new Surface(new SurfaceHandler(colorPicker, spinBoxList));
            hPanel.Children.Add(histogramSurface, true);
        }

        private void SpinBoxOnValueChanged(object sender, EventArgs eventArgs) => histogramSurface.QueueRedrawAll();
    }
}