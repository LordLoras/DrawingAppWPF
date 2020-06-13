using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace DrawingAppWPF
{
    
    class Draw
    {
        Random rand = new Random();

        #region DrawRectangle
        public void DrawRectangle(Canvas drawPanel, string fillColor, string strokeThickness, string height, string width, double opacity)
        {
            Rectangle rect = new Rectangle();
            Brush brush = new SolidColorBrush();
            Color color;
            try
            {
                //try to parse user input into proper color otherwise throw exception   
                color = (Color)ColorConverter.ConvertFromString(fillColor);
                brush = new SolidColorBrush(color);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Invalid Color! Please input new color");
            }
            //Set width and heigth of the rect and the border thickness
            try
            {
                rect.StrokeThickness = int.Parse(strokeThickness);
                rect.Height = int.Parse(height);
                rect.Width = int.Parse(width);
            }
            catch (Exception) { System.Windows.MessageBox.Show("Please enter valid numbers for heighth/width and stroke thickness"); }

            //set border as black color
            rect.Stroke = Brushes.Black;
            //fill the shape with color from string
            rect.Fill = brush;
            //set the opacity
            rect.Opacity = opacity;
            //add the rect to the canvas           
            drawPanel.Children.Add(rect);
            //put the shape in random position after adding it
            RandomPosition(rect);
        }
        #endregion

        #region DrawEllipse
        public void DrawEllipse(Canvas drawPanel, string fillColor, string strokeThickness, string height, string width, double opacity)
        {
            Ellipse ellipse = new Ellipse();
            Brush brush = new SolidColorBrush();
            Color color;
            try
            {
                //try to parse user input into proper color otherwise throw exception   
                color = (Color)ColorConverter.ConvertFromString(fillColor);
                brush = new SolidColorBrush(color);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Invalid Color! Please input new color");
            }
            //Set width and heigth of the ellipse and the border thickness
            try
            {
                ellipse.StrokeThickness = int.Parse(strokeThickness);
                ellipse.Height = int.Parse(height);
                ellipse.Width = int.Parse(width);
            }
            catch (Exception) { System.Windows.MessageBox.Show("Please enter valid numbers for heighth/width and stroke thickness"); }

            //set border as black color
            ellipse.Stroke = Brushes.Black;
            //fill the shape with color from string
            ellipse.Fill = brush;
            //set the opacity
            ellipse.Opacity = opacity;
            //add the rect to the canvas           
            drawPanel.Children.Add(ellipse);
            //put the shape in random position after adding it
            RandomPosition(ellipse);
        }
        #endregion

        #region DrawLine
        public void DrawLine(Canvas drawPanel, string fillColor, string strokeThickness, double opacity)
        {
            Line line = new Line();
            Brush brush = new SolidColorBrush();
            Color color;
            try
            {
                //try to parse user input into proper color otherwise throw exception   
                color = (Color)ColorConverter.ConvertFromString(fillColor);
                brush = new SolidColorBrush(color);
            }
            catch (Exception)
            {
               MessageBox.Show("Invalid Color! Please input new color");
            }
            //Set line thickness
            try
            {
                line.StrokeThickness = int.Parse(strokeThickness);
            }
            catch (Exception) { MessageBox.Show("Please enter valid numbers for heighth/width and stroke thickness"); }

            //set border as black color
            line.Stroke = brush;
            //set the opacity
            line.Opacity = opacity;
            line.X1 = 1;
            line.X2 = rand.Next(1, 500);
            line.Y1 = 1;
            line.Y2 = rand.Next(20, 70);
            //add the rect to the canvas           
            drawPanel.Children.Add(line);
            //put the shape in random position after adding it
            RandomPosition(line);
        }
        #endregion

        #region DrawTriangle
        public void DrawTriangle(Canvas drawPanel, string fillColor, string strokeThickness, double opacity)
        {
            Brush brush = new SolidColorBrush();
            Color color;
            try
            {
                //try to parse user input into proper color otherwise throw exception   
                color = (Color)ColorConverter.ConvertFromString(fillColor);
                brush = new SolidColorBrush(color);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Color! Please input new color");
            }
            Polygon triangle = new Polygon();
            triangle.Stroke = Brushes.Black;
            triangle.Fill = brush;
            triangle.StrokeThickness = 2;
            //make 3 points of a triangle
            Point Point1 = new Point(0,90);
            Point Point2 = new Point(0,0);
            Point Point3 = new Point(90,0);
            PointCollection trianglePoints = new PointCollection();
            //add points to a collection and set them to the triangle
            trianglePoints.Add(Point1);
            trianglePoints.Add(Point2);
            trianglePoints.Add(Point3);
            triangle.Points = trianglePoints;
            //set opacity
            triangle.Opacity = opacity;
            //draw triangle and set random location
            drawPanel.Children.Add(triangle);
            RandomPosition(triangle);        


        }
        #endregion

        public void RandomPosition(Shape shape)
        {
            //Use GUID to generate random seeds otherwise .next() doesn't work properly. Bug?
            Canvas.SetTop(shape, new Random(Guid.NewGuid().GetHashCode()).Next(75, 500));
            Canvas.SetLeft(shape, new Random(Guid.NewGuid().GetHashCode()).Next(0, 1180));
        }

        public void DrawSolution(Canvas drawPanel)
        {
            Polygon shape = new Polygon();
            Polygon shape2 = new Polygon();
            Polygon shape3 = new Polygon();
            shape.Stroke = System.Windows.Media.Brushes.Black;
            shape.Fill = System.Windows.Media.Brushes.LightSeaGreen;
            shape.StrokeThickness = 2;
            PointCollection myPointCollection = new PointCollection();
            Point Point1 = new Point(0, 0);
            Point Point2 = new Point(50, 0);
            Point Point3 = new Point(50, 40);
            Point Point4 = new Point(0, 40);
            myPointCollection.Add(Point1);
            myPointCollection.Add(Point2);
            myPointCollection.Add(Point3);
            myPointCollection.Add(Point4);
            shape.Points = myPointCollection;
            drawPanel.Children.Add(shape);

            Point1 = new Point(50, 0);
            Point2 = new Point(100, 0);
            Point3 = new Point(100, 40);
            Point4 = new Point(50, 40);
            myPointCollection.Add(Point1);
            myPointCollection.Add(Point2);
            myPointCollection.Add(Point3);
            myPointCollection.Add(Point4);
            shape.Points = myPointCollection;
            drawPanel.Children.Add(shape2);

            Point1 = new Point(0, 40);
            Point2 = new Point(100, 40);
            Point3 = new Point(100, 80);
            Point4 = new Point(0, 80);

            
            myPointCollection.Add(Point1);
            myPointCollection.Add(Point2);
            myPointCollection.Add(Point3);
            myPointCollection.Add(Point4);
            shape.Points = myPointCollection;
            drawPanel.Children.Add(shape3);




        }
    
    }
}