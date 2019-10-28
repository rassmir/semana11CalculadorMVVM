using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator.ViewModels
{
    public class OperacionesViewModel : ViewModelBase
    {
        string Resultado = "0";
        string historyString = "";
        bool isSumDisplayed = false;
        double accumulatedSum = 0;

        public OperacionesViewModel()
        {

           
            NumeroCommand = new Command<string>(
                 execute: (string parameter) =>
                 {
                     if (isSumDisplayed || Resultado == "0")
                         Resultado = parameter;
                     else
                         Resultado += parameter;

                     isSumDisplayed = false;
                     RefreshCanExecutes();
                 },
                 canExecute: (string parameter) =>
                 {
                     return isSumDisplayed || Resultado.Length < 16;
                 });

            SumarCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(Resultado);
                    HistoryString += value.ToString() + " + ";
                    accumulatedSum += value;
                    Resultado = accumulatedSum.ToString();
                    isSumDisplayed = true;
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return !isSumDisplayed;
                });

        }

        void RefreshCanExecutes()
        {
            ((Command)NumeroCommand).ChangeCanExecute();
            ((Command)SumarCommand).ChangeCanExecute();

        }

        public string HistoryString
        {
            private set { SetProperty(ref historyString, value); }
            get { return historyString; }
        }

        public ICommand NumeroCommand { private set; get; }
        public ICommand SumarCommand { private set; get; }

    }
}

