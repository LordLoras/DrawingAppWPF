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


namespace DrawingAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Draw draw = new Draw();
        //LMB Down
        bool mouseDown = false;
        //UIelement under mouse
        UIElement currElement;

        public MainWindow()
        {
            InitializeComponent();
          
  
            
        }

        #region DrawButton_Click
        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            //Call the corresponding method based on the combo box
            switch(shapeBox.Text)
            {
                case "Rectangle":
                draw.DrawRectangle(drawPanel, colorBox.Text, strokeThickness.Text, heightBox.Text, widthBox.Text, opacitySlider.Value);
                    break;

                case "Ellipse":
                draw.DrawEllipse(drawPanel, colorBox.Text, strokeThickness.Text, heightBox.Text, widthBox.Text, opacitySlider.Value);
                    break;
                case "Line":
                    draw.DrawLine(drawPanel, colorBox.Text, strokeThickness.Text, opacitySlider.Value);
                    break;

                case "Triangle":
                    draw.DrawTriangle(drawPanel,colorBox.Text,strokeThickness.Text,opacitySlider.Value);
                    break;
            }


        }
        #endregion

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //update the label for visualizing % opacity
            opacityLabel.Content = Math.Round(opacitySlider.Value, 2).ToString();
           
        }
        //read the xaml from filestream
        public static Canvas LoadCanvas()
        {
            using (System.IO.FileStream fs = System.IO.File.Open("save.xaml", System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                return System.Windows.Markup.XamlReader.Load(fs) as Canvas;
            }
        }
        private void LoadBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //check if the save file exist
                if(System.IO.File.Exists("save.xaml"))
                {
                    //clear any existing shapes
                    drawPanel.Children.Clear();
                    
                    //load shapes from file
                    drawPanel.Children.Add(LoadCanvas());
                    Canvas canvas = LoadCanvas();
                   
                }
                else
                {
                    MessageBox.Show("Save file doesn't exist. Save before loading!");
                }

            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //store the canvas and its children markup in string and write it to file.
                string xaml = System.Windows.Markup.XamlWriter.Save(drawPanel);
                System.IO.FileStream fs = System.IO.File.Create("save.xaml");
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
                sw.Write(xaml);            
                sw.Close();
                fs.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        #region drawPanel LMBDown
        private void DrawPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //set mouse down state to true
            mouseDown = true;
            //get the UI element thats over the cursor and stick the mouse to the element.
            currElement = Mouse.DirectlyOver as UIElement;
            currElement.CaptureMouse();
            
        }
        #endregion


        #region drawPanel LMBUP
        private void DrawPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //set mouse down state to false
            mouseDown = false;
            //release the mouse after LMB is let go
            currElement.ReleaseMouseCapture();

            //Delete operation on LMB UP
            if (operationBox.SelectedIndex == 3)
            {
                drawPanel.Children.Remove(currElement);
            }
        }
        #endregion

        #region drawPanel MouseMove
        private void DrawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            //if the user is holding LMB down
            if(mouseDown)
            {
                //get the mouse position and set the element position based on movement
                Point mousePosition = e.GetPosition(drawPanel);

                switch(operationBox.Text)
                {
                    case "Move":
                        Canvas.SetTop(currElement, mousePosition.Y);
                        Canvas.SetLeft(currElement, mousePosition.X);
                        break;
                    //Rotating is not working as intended due to needing more advanced math on polygons and different shape sizes
                    case "Rotate":
                        currElement.RenderTransform = new RotateTransform(mousePosition.X);
                        break;
                    //TO-DO
                    case "Scale":
                        break;
                  
                        
                }
                //test

                testLBL.Content = "X: " + mousePosition.X.ToString() + " " + "Y: " + mousePosition.Y.ToString();
                
            }
        }
        #endregion

        #region KeyUp KeyHook
        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            //draw a rectangle with key press F1
            if(e.Key == Key.F1)
            {
                draw.DrawRectangle(drawPanel, colorBox.Text, strokeThickness.Text, heightBox.Text, widthBox.Text, opacitySlider.Value);
            }
            //Select operation to delete with F2
            if(e.Key == Key.F2)
            {
                operationBox.SelectedIndex = 3;
            }
            //select operation to move with F3
            if(e.Key == Key.F3)
            {
                operationBox.SelectedIndex = 1;
            }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            draw.DrawSolution(drawPanel);
        }
    }
}
