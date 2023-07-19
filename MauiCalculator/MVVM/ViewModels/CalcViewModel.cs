using Dangl.Calculator;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiCalculator.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CalcViewModel
    {
        public string Formula { get; set; } = "";
        public string Result { get; set; } = "0";
        private List<string> operationCommands = new List<string>() {"/","*","-","+","%" };
        public ICommand OperationCommand =>
            new Command((number) => 
            {
                if(Formula.Length > 0)
                {
                    
                    if (operationCommands.Contains(number.ToString().Trim()) && Formula.Length > 0 && string.IsNullOrWhiteSpace(Formula[Formula.Length - 1].ToString()))
                    {
                        Formula = Formula.Substring(0, Formula.Length - 3);
                    }
                    
                    
                   
                }
                bool exists = false;
                operationCommands.ForEach(c => { if (!exists) exists = Formula.Contains(c); });
                if (!operationCommands.Contains(number) && exists)
                {
                    Formula += number;
                    var calculation = Calculator.Calculate(Formula);
                    Formula = Result = calculation.Result.ToString();
                    exists = false;
                }
                else if (!(number.Equals(".") && Formula.Contains(".")))
                {
                    Formula += number;
                }
                
            });
        public ICommand ResetOperation =>
            new Command(() => { Formula = ""; Result = "0"; });
        public ICommand BackSpaceCommand => new Command(() =>
        {
            if (Formula.Length > 0 && string.IsNullOrWhiteSpace(Formula[Formula.Length - 1].ToString()))
            {
                Formula = Formula.Substring(0, Formula.Length - 3);
            }
            else if (Formula.Length > 0)
            {
                Formula = Formula.Substring(0, Formula.Length - 1);
            }
        });

        public ICommand CalculateCommand => new Command(() =>
        {
            if (Formula.Length == 0)
                return;
            var calculation = Calculator.Calculate(Formula);
            Result= calculation.Result.ToString();
        });
    }
}
