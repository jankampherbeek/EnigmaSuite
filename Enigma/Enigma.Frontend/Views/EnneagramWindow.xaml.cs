// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Documents;
using Enigma.Frontend.Ui.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls.Primitives;

namespace Enigma.Frontend.Ui.Views;

public partial class EnneagramWindow : Window
{
    private EnneagramViewModel _viewModel;

    public EnneagramWindow()
    {
        InitializeComponent();
        _viewModel = App.ServiceProvider.GetRequiredService<EnneagramViewModel>();
        DataContext = _viewModel;
        
        // Listen for canvas redraw notifications
        _viewModel.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(_viewModel.CanvasNeedsRedraw))
            {
                DrawEnneagram();
            }
        };
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (_viewModel != null)
        {
            _viewModel.OnWindowResize(EnneagramCanvas.ActualWidth, EnneagramCanvas.ActualHeight);
        }
    }

    private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (_viewModel != null)
        {
            _viewModel.OnWindowResize(e.NewSize.Width, e.NewSize.Height);
        }
    }

    private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        // Find the ListViewItem that was clicked
        var originalSource = e.OriginalSource as DependencyObject;
        while (originalSource != null && !(originalSource is ListViewItem))
        {
            originalSource = VisualTreeHelper.GetParent(originalSource);
        }

        if (originalSource is ListViewItem item && item.DataContext is EnneagramTypeResult result)
        {
            ShowEnneagramTypePopup(result.Type, result.Name, result.TooltipText);
            e.Handled = true; // Prevent the ListView from processing the selection
        }
    }

    private void ShowEnneagramTypePopup(int type, string name, string content)
    {
        var titleText = $"{type} - {name}";
        var contentText = content ?? "No detailed information available for this Enneagram type.";
        
        PopupTitle.Text = titleText;
        PopupContent.Text = contentText;
        
        // Force layout update
        PopupContent.InvalidateVisual();
        PopupContent.UpdateLayout();
        
        PopupOverlay.Visibility = Visibility.Visible;
        
        // Position popup near the mouse cursor
        var mousePosition = Mouse.GetPosition(this);
        EnneagramTypePopup.HorizontalOffset = mousePosition.X + 10;
        EnneagramTypePopup.VerticalOffset = mousePosition.Y + 10;
        
        EnneagramTypePopup.IsOpen = true;
    }

    private void Popup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        // Close popup when clicking on it
        EnneagramTypePopup.IsOpen = false;
        PopupOverlay.Visibility = Visibility.Collapsed;
        e.Handled = true;
    }

    private void Overlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        // Close popup when clicking on overlay
        EnneagramTypePopup.IsOpen = false;
        PopupOverlay.Visibility = Visibility.Collapsed;
    }

    private void DrawEnneagram()
    {
        EnneagramCanvas.Children.Clear();

        if (_viewModel?.EnneagramCircles == null || !_viewModel.EnneagramCircles.Any())
            return;

        // Draw lines first (so they appear behind circles)
        foreach (var line in _viewModel.EnneagramLines)
        {
            var fromCircle = _viewModel.EnneagramCircles.FirstOrDefault(c => c.Type == line.FromType);
            var toCircle = _viewModel.EnneagramCircles.FirstOrDefault(c => c.Type == line.ToType);

            if (fromCircle != null && toCircle != null)
            {
                var lineElement = new Line
                {
                    X1 = fromCircle.X,
                    Y1 = fromCircle.Y,
                    X2 = toCircle.X,
                    Y2 = toCircle.Y,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };
                EnneagramCanvas.Children.Add(lineElement);
            }
        }

        // Draw circles
        foreach (var circle in _viewModel.EnneagramCircles)
        {
            // Create circle
            var ellipse = new Ellipse
            {
                Width = circle.Radius * 2,
                Height = circle.Radius * 2,
                Fill = new SolidColorBrush(circle.Color),
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                IsHitTestVisible = true,
                Focusable = true
            };

            // Create a border to host the ellipse and add click functionality
            var border = new Border
            {
                Width = ellipse.Width,
                Height = ellipse.Height,
                Background = Brushes.Transparent, // ensure hit test
                Child = ellipse,
                Cursor = Cursors.Hand
            };

            // Add click event handler
            border.MouseLeftButtonDown += (sender, e) =>
            {
                ShowEnneagramTypePopup(circle.Type, circle.Name, circle.Tooltip);
            };

            Canvas.SetLeft(border, circle.X - circle.Radius);
            Canvas.SetTop(border, circle.Y - circle.Radius);

            EnneagramCanvas.Children.Add(border);

            // Add number text at center of circle
            var numberText = new TextBlock
            {
                Text = circle.Type.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Canvas.SetLeft(numberText, circle.X - 10);
            Canvas.SetTop(numberText, circle.Y - 10);

            EnneagramCanvas.Children.Add(numberText);

            // Add name text with specific positioning rules
            var nameText = new TextBlock
            {
                Text = circle.Name,
                FontSize = 10,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                MaxWidth = circle.Radius * 3
            };

            // Position text based on circle number
            double textX, textY;
            
            if (circle.Type == 7 || circle.Type == 8 || circle.Type == 9 || circle.Type == 1 || circle.Type == 2)
            {
                // Text above circles 7, 8, 9, 1, 2
                textX = circle.X - (circle.Radius * 1.5);
                textY = circle.Y - circle.Radius - 25;
            }
            else
            {
                // Text under other circles (3-6)
                textX = circle.X - (circle.Radius * 1.5);
                textY = circle.Y + circle.Radius + 5;
            }

            // For circles 1, 2, 3, start text at center of circle
            if (circle.Type == 1 || circle.Type == 2 || circle.Type == 3)
            {
                textX = circle.X;
            }

            Canvas.SetLeft(nameText, textX);
            Canvas.SetTop(nameText, textY);

            EnneagramCanvas.Children.Add(nameText);
        }
    }
}