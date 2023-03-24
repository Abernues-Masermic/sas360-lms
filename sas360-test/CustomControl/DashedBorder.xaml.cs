using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sas360_test.CustomControl
{
    public partial class DashedBorder : Border
    {
        public DashedBorder()
        {
            InitializeComponent();
        }
    
        private static DoubleCollection? emptyDoubleCollection;
        private static DoubleCollection EmptyDoubleCollection()
        {
            if (emptyDoubleCollection == null)
            {
                DoubleCollection doubleCollection = new DoubleCollection();
                doubleCollection.Freeze();
                emptyDoubleCollection = doubleCollection;
            }
            return emptyDoubleCollection;
        }

        public static readonly DependencyProperty UseDashedBorderProperty =
          DependencyProperty.Register(nameof(UseDashedBorder),
                                      typeof(bool),
                                      typeof(DashedBorder),
                                      new FrameworkPropertyMetadata(false, OnUseDashedBorderChanged));

        public static readonly DependencyProperty DashedBorderBrushProperty =
          DependencyProperty.Register(nameof(DashedBorderBrush),
                                      typeof(Brush),
                                      typeof(DashedBorder),
                                      new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty StrokeDashArrayProperty =
          DependencyProperty.Register(nameof(StrokeDashArray),
                                      typeof(DoubleCollection),
                                      typeof(DashedBorder),
                                      new FrameworkPropertyMetadata(EmptyDoubleCollection()));

        private static void OnUseDashedBorderChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            DashedBorder dashedBorder = (DashedBorder)target;
            dashedBorder.UseDashedBorderChanged();
        }

        private Rectangle GetBoundRectangle()
        {
            Rectangle rectangle = new();

            rectangle.SetBinding(Shape.StrokeThicknessProperty, new Binding() { Source = this, Path = new PropertyPath("BorderThickness.Left") });
            rectangle.SetBinding(Rectangle.RadiusXProperty, new Binding() { Source = this, Path = new PropertyPath("CornerRadius.TopLeft") });
            rectangle.SetBinding(Rectangle.RadiusYProperty, new Binding() { Source = this, Path = new PropertyPath("CornerRadius.TopLeft") });
            rectangle.SetBinding(WidthProperty, new Binding() { Source = this, Path = new PropertyPath(ActualWidthProperty) });
            rectangle.SetBinding(HeightProperty, new Binding() { Source = this, Path = new PropertyPath(ActualHeightProperty) });

            return rectangle;
        }

        private Rectangle GetBackgroundRectangle()
        {
            Rectangle rectangle = GetBoundRectangle();
            rectangle.SetBinding(Rectangle.StrokeProperty, new Binding() { Source = this, Path = new PropertyPath(BackgroundProperty) });
            return rectangle;
        }

        private Rectangle GetDashedRectangle()
        {
            Rectangle rectangle = GetBoundRectangle();
            rectangle.SetBinding(Shape.StrokeDashArrayProperty, new Binding() { Source = this, Path = new PropertyPath(StrokeDashArrayProperty) });
            rectangle.SetBinding(Shape.StrokeProperty, new Binding() { Source = this, Path = new PropertyPath(DashedBorderBrushProperty) });
            Panel.SetZIndex(rectangle, 2);
            return rectangle;
        }

        private VisualBrush CreateDashedBorderBrush()
        {
            VisualBrush dashedBorderBrush = new VisualBrush();
            Grid grid = new();
            Rectangle backgroundRectangle = GetBackgroundRectangle();
            Rectangle dashedRectangle = GetDashedRectangle();
            grid.Children.Add(backgroundRectangle);
            grid.Children.Add(dashedRectangle);
            dashedBorderBrush.Visual = grid;
            return dashedBorderBrush;
        }

        private void UseDashedBorderChanged()
        {
            if (UseDashedBorder)
            {
                BorderBrush = CreateDashedBorderBrush();
            }
            else
            {
                ClearValue(BorderBrushProperty);
            }
        }

        public bool UseDashedBorder
        {
            get { return (bool)GetValue(UseDashedBorderProperty); }
            set { SetValue(UseDashedBorderProperty, value); }
        }

        public Brush DashedBorderBrush
        {
            get { return (Brush)GetValue(DashedBorderBrushProperty); }
            set { SetValue(DashedBorderBrushProperty, value); }
        }

        public DoubleCollection StrokeDashArray
        {
            get { return (DoubleCollection)GetValue(StrokeDashArrayProperty); }
            set { SetValue(StrokeDashArrayProperty, value); }
        }

    }
}
