using MauiCalculator.MVVM.ViewModels;

namespace MauiCalculator.MVVM.Views;

public partial class CalcView : ContentPage
{
	public CalcView()
	{
		InitializeComponent();
		BindingContext = new CalcViewModel();
	}
}