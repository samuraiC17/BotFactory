using BotFactory.Common.Interface;
using BotFactory.Tools;
using System.Windows;
using System.Windows.Controls;

namespace BotFactory.Pages
{
    /// <summary>
    /// Interaction logic for UnitTest.xaml
    /// </summary>
    public partial class UnitTest : Page
    {
        private UnitDataContext _unitDataContext = new UnitDataContext();

        public UnitTest()
        {
            InitializeComponent();

            DataContext = _unitDataContext;
        }

        public void SetUnitToTest(ITestingUnit unit)
        {
            _unitDataContext.IBot = unit;
        }
        
        private async void ButtonWork_Click(object sender, RoutedEventArgs e)
        {
            if (_unitDataContext.IBot != null)
            {
                newState("a bien reçus l'ordre d'aller au travail.");
                var response = await _unitDataContext.IBot.WorkBegins();
                _unitDataContext.Response = response;
                _unitDataContext.Working = _unitDataContext.IBot.IsWorking;
                _unitDataContext.CurrentPos = _unitDataContext.IBot.CurrentPos;
                newState(_unitDataContext.Response ? "est à son poste de travail." : "n'est pas aller son poste de travail.");

            }
        }

        private async void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            if (_unitDataContext.IBot != null)
            {
                newState("a bien reçus l'ordre d'aller à sa place de parking.");
                var response = await _unitDataContext.IBot.WorkEnds();
                _unitDataContext.Response = response;
                _unitDataContext.Working = _unitDataContext.IBot.IsWorking;
                _unitDataContext.CurrentPos = _unitDataContext.IBot.CurrentPos;
                newState(_unitDataContext.Response ? "est à sa place de parking." : "n'est pas allé  a sa place de parking.");
            }
        }

        private void newState (string message)
        {
            Reporting.StatusChangedEventArgs a = new Reporting.StatusChangedEventArgs() { NewStatus = _unitDataContext.IBot.Name + " " +  message.Trim() };
            _unitDataContext.IBot.UnitStatusChanged(_unitDataContext.IBot, a as IStatusChangedEventArgs);
        }
    }
}
