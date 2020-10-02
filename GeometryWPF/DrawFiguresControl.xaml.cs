using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary_CalculateFiguresArea;

namespace GeometryWPF
{
    /// <summary>
    /// Логика взаимодействия для DrayFiguresControl.xaml
    /// </summary>
    public partial class DrawFiguresControl : UserControl
    {
        public DrawFiguresControl()
        {
            InitializeComponent();
            FiguresData fd = new FiguresData();
            Circle = new Circle(fd);
            Triangle = new Triangle(fd);
            Rectangle = new ClassLibrary_CalculateFiguresArea.Rectangle(fd);
            FiguresList = new List<string>();
            FiguresList.Add("Not selected");
            FiguresList.AddRange(Enum.GetNames(typeof(Figures)));
            FigureTypesComboBox.ItemsSource = FiguresList;
            FigureTypesComboBox.SelectedIndex = 0;
        }

        #region Global Variables
        private List<string> FiguresList { get; set; }
        private ClassLibrary_CalculateFiguresArea.Rectangle Rectangle { get; set; }
        private Circle Circle { get; set; }
        private Triangle Triangle { get; set; }
        private Ellipse el { get; set; }
        private Polygon pg { get; set; }
        private System.Windows.Shapes.Rectangle rg { get; set; }

        #endregion

        private void FigureTypesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearInputFields();
            ClearDrawArea();
            if (FigureTypesComboBox.SelectedIndex == 0)
            {
                SideA_Value.IsEnabled = false;
                SideB_Value.IsEnabled = false;
                SideC_Value.IsEnabled = false;
                SideA_Tip.Content = "Введите длину:";
                SideB_Tip.Content = "Введите длину:";
                SideC_Tip.Content = "Введите длину:";
                DrawButton.IsEnabled = false;
            }
            else if (FigureTypesComboBox.SelectedIndex == 1)
            {
                SideA_Value.IsEnabled = true;
                SideB_Value.IsEnabled = false;
                SideC_Value.IsEnabled = false;
                SideA_Tip.Content = "Введите радиус круга:";
                FiguresData fd = new FiguresData();
                Circle = new Circle(fd);
                DrawButton.IsEnabled = true;
            }
            else if (FigureTypesComboBox.SelectedIndex == 2)
            {
                SideA_Value.IsEnabled = true;
                SideB_Value.IsEnabled = true;
                SideC_Value.IsEnabled = false;
                SideA_Tip.Content = "Введите длину стороны A:";
                SideB_Tip.Content = "Введите длину стороны B:";
                SideC_Tip.Content = "Длина стороны C:";
                FiguresData fd = new FiguresData();
                Triangle = new Triangle(fd);
                DrawButton.IsEnabled = true;
            }
            else if (FigureTypesComboBox.SelectedIndex == 3)
            {
                SideA_Value.IsEnabled = true;
                SideB_Value.IsEnabled = true;
                SideC_Value.IsEnabled = false;
                SideA_Tip.Content = "Введите длину стороны A:";
                SideB_Tip.Content = "Введите длину стороны B:";
                FiguresData fd = new FiguresData();
                Rectangle = new ClassLibrary_CalculateFiguresArea.Rectangle(fd);
                DrawButton.IsEnabled = true;
            }
        }

        private void DrawFigure_Click(object sender, RoutedEventArgs e)
        {
            if (FigureTypesComboBox.SelectedIndex == 1)
            {
                ExternalGrid.Children.Remove(el);
                el = new Ellipse();
                el.Width = el.Height = Circle.R * 2;
                el.VerticalAlignment = VerticalAlignment.Center;
                el.HorizontalAlignment = HorizontalAlignment.Center;
                el.Fill = Brushes.Gray;
                ExternalGrid.Children.Add(el);
                Grid.SetColumn(el, 1);
            }
            else if (FigureTypesComboBox.SelectedIndex == 2)
            {
                ExternalGrid.Children.Remove(pg);
                pg = new Polygon();
                System.Windows.Point p1 = new System.Windows.Point();
                p1.X = 0; p1.Y = 0;
                System.Windows.Point p3 = new System.Windows.Point();
                p3.X = (int)Triangle.A; p3.Y = 0;
                System.Windows.Point p2 = new System.Windows.Point();
                p2.X = (int)(Math.Sqrt(Math.Pow(Triangle.C, 2) - Math.Pow((2 * Triangle.Area() / Triangle.A), 2))); p2.Y = -(int)(2 * Triangle.Area() / Triangle.A);
                pg.Points = new PointCollection() { p1, p2, p3 };
                pg.VerticalAlignment = VerticalAlignment.Center;
                pg.HorizontalAlignment = HorizontalAlignment.Center;
                pg.Fill = Brushes.Gray;
                ExternalGrid.Children.Add(pg);
                Grid.SetColumn(pg, 1);
            }
            else if (FigureTypesComboBox.SelectedIndex == 3)
            {
                ExternalGrid.Children.Remove(rg);
                rg = new System.Windows.Shapes.Rectangle();
                rg.Width = Rectangle.A;
                rg.Height = Rectangle.B;
                rg.Fill = Brushes.Gray;
                ExternalGrid.Children.Add(rg);
                Grid.SetColumn(rg, 1);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (FigureTypesComboBox.SelectedIndex == 1) ExternalGrid.Children.Remove(el);
            if (FigureTypesComboBox.SelectedIndex == 2) ExternalGrid.Children.Remove(pg);
            if (FigureTypesComboBox.SelectedIndex == 3) ExternalGrid.Children.Remove(rg);
        }

        private void SideA_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FigureTypesComboBox.SelectedIndex == 1)
            {
                
                if (!String.IsNullOrEmpty(SideA_Value.Text) && !String.IsNullOrWhiteSpace(SideA_Value.Text))
                {
                    Circle.R = double.Parse(SideA_Value.Text);
                    Area_Value.Text = Circle.Area().ToString();
                }
            }
            if (FigureTypesComboBox.SelectedIndex == 2)
            {
                if (!String.IsNullOrEmpty(SideA_Value.Text) && !String.IsNullOrWhiteSpace(SideA_Value.Text))
                {
                    Triangle.A = double.Parse(SideA_Value.Text);
                    if (Triangle.A != 0 && Triangle.B != 0)
                    {
                        Triangle.C = Triangle.CalculateSideLength(Triangle.A, Triangle.B, 45);
                        SideC_Value.Text = Triangle.C.ToString();
                        Area_Value.Text = Triangle.Area().ToString();
                    }
                }
            }
            if (FigureTypesComboBox.SelectedIndex == 3)
            {
                if (!String.IsNullOrEmpty(SideA_Value.Text) && !String.IsNullOrWhiteSpace(SideA_Value.Text))
                    Rectangle.A = double.Parse(SideA_Value.Text);
                    Area_Value.Text = Rectangle.Area().ToString();
            }
        }

        private void SideB_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FigureTypesComboBox.SelectedIndex == 2)
            {
                if (!String.IsNullOrEmpty(SideB_Value.Text) && !String.IsNullOrWhiteSpace(SideB_Value.Text))
                {
                    Triangle.B = double.Parse(SideB_Value.Text);
                    if (Triangle.A != 0 && Triangle.B != 0)
                    {
                        Triangle.C = Triangle.CalculateSideLength(Triangle.A, Triangle.B, 45);
                        SideC_Value.Text = Triangle.C.ToString();
                        Area_Value.Text = Triangle.Area().ToString();
                    }
                }
            }
            if (FigureTypesComboBox.SelectedIndex == 3)
            {
                if (!String.IsNullOrEmpty(SideB_Value.Text) && !String.IsNullOrWhiteSpace(SideB_Value.Text))
                {
                    Rectangle.B = double.Parse(SideB_Value.Text);
                    Area_Value.Text = Rectangle.Area().ToString();
                }
            }
        }
        private void ClearDrawArea()
        {
            ExternalGrid.Children.Remove(el);
            ExternalGrid.Children.Remove(pg);
            ExternalGrid.Children.Remove(rg);
        }

        private void ClearInputFields()
        {
            SideA_Value.Text = "0";
            SideB_Value.Text = "0";
            SideC_Value.Text = "0";
            Area_Value.Text = "0";
        }
    }
}
