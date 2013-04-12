using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MyEd
{
	public partial class MainWindow : Window
	{

		#region Text formatting functions

		private void Bold_Click(object sender, RoutedEventArgs e)
		{
			object currentWeight = EdBox.Selection.GetPropertyValue(FlowDocument.FontWeightProperty);
			FontWeight newWeight = FontWeights.Bold;
			if (currentWeight is FontWeight)
			{
				if ((FontWeight)currentWeight == FontWeights.Bold)
				{
					newWeight = FontWeights.Normal;
				}
				else if ((FontWeight)currentWeight == FontWeights.Normal)
				{
					newWeight = FontWeights.Bold;
				}
			}

			EdBox.Selection.ApplyPropertyValue(FlowDocument.FontWeightProperty, newWeight);
		}

		private void Italic_Click(object sender, RoutedEventArgs e)
		{
			object fontStyle = EdBox.Selection.GetPropertyValue(FlowDocument.FontStyleProperty);
			FontStyle newFontStyle = FontStyles.Italic;
			if (fontStyle is FontStyle)
			{
				if ((FontStyle)fontStyle == FontStyles.Italic)
				{
					newFontStyle = FontStyles.Normal;
				}
				else if ((FontStyle)fontStyle == FontStyles.Normal)
				{
					newFontStyle = FontStyles.Italic;
				}
			}

			EdBox.Selection.ApplyPropertyValue(FlowDocument.FontStyleProperty, newFontStyle);
		}

		private void Underline_Click(object sender, RoutedEventArgs e)
		{
			object currentTextDecoration = EdBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
			TextDecorationCollection newTextDecoration = TextDecorations.Underline;
			if (currentTextDecoration is TextDecorationCollection)
			{
				if (currentTextDecoration == TextDecorations.Underline)
				{
					newTextDecoration = new TextDecorationCollection();
				}
				else if (currentTextDecoration == TextDecorations.Baseline)
				{
					newTextDecoration = TextDecorations.Underline;
				}
			}

			EdBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, newTextDecoration);
		}

		private void ClearFormat_Click(object sender, RoutedEventArgs e)
		{
			EdBox.Selection.ClearAllProperties();
		}

		private void pt12_Click(object sender, RoutedEventArgs e)
		{
			EdBox.Selection.ApplyPropertyValue(FlowDocument.FontSizeProperty, 12.0 * Pt);
		}

		private void pt14_Click(object sender, RoutedEventArgs e)
		{
			EdBox.Selection.ApplyPropertyValue(FlowDocument.FontSizeProperty, 14.0 * Pt);
		}

		private void pt16_Click(object sender, RoutedEventArgs e)
		{
			EdBox.Selection.ApplyPropertyValue(FlowDocument.FontSizeProperty, 16.0 * Pt);
		}

		private void pt20_Click(object sender, RoutedEventArgs e)
		{
			EdBox.Selection.ApplyPropertyValue(FlowDocument.FontSizeProperty, 20.0 * Pt);
		}

		private void pt24_Click(object sender, RoutedEventArgs e)
		{
			EdBox.Selection.ApplyPropertyValue(FlowDocument.FontSizeProperty, 24.0 * Pt);
		}

		private void FontSize_TextChanged(object sender, TextChangedEventArgs e)
		{
			double textSize = 12;
			string text = ((TextBox)sender).Text;
			try
			{
				textSize = Double.Parse(text);
			}
			catch
			{
			}

			EdBox.Selection.ApplyPropertyValue(FlowDocument.FontSizeProperty, textSize * Pt);
		}

		private void LineHeight1_Click(object sender, RoutedEventArgs e)
		{
			EdBox.Document.LineHeight = 1;
		}

		private void LineHeight2_Click(object sender, RoutedEventArgs e)
		{
			EdBox.Document.LineHeight = 4;
		}

		private void LineHeight3_Click(object sender, RoutedEventArgs e)
		{
			EdBox.Document.LineHeight = 8;
		}

		#endregion
		

	}
}
