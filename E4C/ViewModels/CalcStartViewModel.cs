// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Views;

namespace E4C.ViewModels
{

    public interface ICalcStartViewModel
    {
        public void ShowCalcJd();
        public void ShowCalcObliquity();

    }

    public class CalcStartViewModel : ICalcStartViewModel
    {
        readonly private CalcJdView _calcJdView;
        readonly private CalcObliquityView _calcOblView;

        public CalcStartViewModel(CalcJdView calcJdView, CalcObliquityView calcOblView)
        {
            _calcJdView = calcJdView;
            _calcOblView = calcOblView;
        }

        public void ShowCalcJd()
        {
            _calcJdView.Show();
        }

        public void ShowCalcObliquity()
        {
            _calcOblView.Show();
        }

    }
}
