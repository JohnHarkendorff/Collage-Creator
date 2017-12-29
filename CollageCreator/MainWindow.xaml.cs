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

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            var fileWasChosen = dlg.ShowDialog();

            if (fileWasChosen == true)
            {
                var chosenImage = new BitmapImage(new Uri(dlg.FileName));
                var image = new Image { Source = chosenImage };
                
                if (image1.Source == null)
                {
                    image1.Source = chosenImage;
                    SetSize(image1);
                }
                else if (image2.Source == null)
                {
                    image2.Source = chosenImage;
                    SetSize(image2);
                }
                else if (image3.Source == null)
                {
                    image3.Source = chosenImage;
                    SetSize(image3);
                }
                else if (image4.Source == null)
                {
                    image4.Source = chosenImage;
                    SetSize(image4);
                }
                else if (image5.Source == null)
                {
                    image5.Source = chosenImage;
                    SetSize(image5);
                }

            }
        }

        private void SetSize(Image image)
        {
            if (image.Source.Height > 600 || image.Source.Width > 800)
            {
                image.Height = image.Source.Height;
                image.Width = image.Source.Width;
                while (image.Height > 600 || image.Width > 800)
                {
                    image.Height = image.Height / 2;
                    image.Width = image.Width / 2;
                }
            }            
        }

        private void DeletePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Image image in canvas.Children)
            {
                if (image.Source != null)
                {
                    image.Source = new BitmapImage(new Uri("Media/green_plus.png", UriKind.Relative));
                }
                
            }
        }

        private Control draggedItem;
        private Point mousePosition;

        private void CanvasMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            draggedItem = sender as Button;
            if(canvas.CaptureMouse())
            {
                mousePosition = e.GetPosition(draggedItem);
            }
        }

        private void CanvasMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (draggedItem != null)
            {
                canvas.ReleaseMouseCapture();
                Panel.SetZIndex(draggedItem, 0);
                draggedItem = null;
            }
        }

        private void CanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (draggedItem != null)
            {
                var position = e.GetPosition(canvas);
                var offset = position - mousePosition;
                mousePosition = position;
                //image1.Margin = new Thickness(offset.X, offset.Y, 0, 0);
                Canvas.SetLeft(draggedItem, Canvas.GetLeft(draggedItem) + offset.X);
                Canvas.SetTop(draggedItem, Canvas.GetTop(draggedItem) + offset.Y);
                //Canvas.GetTop(image1) + offset.Y);
            }
        }

        private void ChangeImage(object sender, RoutedEventArgs e)
        {
            Button1.Content = new BitmapImage(new Uri("Media/Bin-512.png", UriKind.Relative));
        }
    }
}
