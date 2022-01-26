using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E4C.Views;

namespace E4C.ViewModels
{

    public interface ICalcStartViewModel
    {
        public void ShowCalcJd();
        public void ShowCalcObliquity();

    }

    public class CalcStartViewModel: ICalcStartViewModel
    {
        readonly private CalcJdView calcJdView;
        readonly private CalcObliquityView calcOblView;

        public CalcStartViewModel(CalcJdView calcJdView, CalcObliquityView calcOblView)
        {
            this.calcJdView = calcJdView;
            this.calcOblView = calcOblView;
        }

        public void ShowCalcJd()
        {
            calcJdView.Show();
        }

        public void ShowCalcObliquity()
        {
            calcOblView.Show();
        }

    }
}
