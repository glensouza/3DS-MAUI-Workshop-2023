using CommunityToolkit.Mvvm.ComponentModel;

namespace CheckSplitter.ViewModels;

public partial class CheckViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Total))]
    [NotifyPropertyChangedFor(nameof(Tip))]
    [NotifyPropertyChangedFor(nameof(PersonalAmount))]
    double _amount;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Total))]
    [NotifyPropertyChangedFor(nameof(Tip))]
    [NotifyPropertyChangedFor(nameof(PersonalAmount))]
    double _tipRate;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PersonalAmount))]
    int _numPeople = 1;

    public double Tip => RoundUp(Amount * (TipRate / 100.0));
    public double Total => RoundUp(Amount + Tip);
    public double PersonalAmount => RoundUp(Total / (NumPeople * 1.0));

    private double RoundUp(double amount)
    {
        decimal n = 0;

        try
        {
            n = (decimal)amount;
            n = System.Math.Ceiling(n * 100) / 100;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return (double)n;
    }
}
