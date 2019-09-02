using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MainWindowApp {
    /// <summary>
    /// Some Logic
    /// </summary>
    public partial class MainWindow : Window {

        //manager for blocking/ublocking the ports
        GTA5Session manager = new GTA5Session();

        public MainWindow() {
            InitializeComponent();
        }

        private void BlockButton_Click(object sender, RoutedEventArgs e) {

            //blocking this button 
            BlockButton.IsEnabled = false;

            //checking if the disable button is disabled; if so, enable it
            if (!UnblockButton.IsEnabled) UnblockButton.IsEnabled = true;

            //Using the block method on the manager 
            manager.Block();
            
            //Chaning the status to active
            ActiveLabel.Content = "Active!";
            ActiveLabel.Foreground = Brushes.Green;
          
        }

        private void UnblockButton_Click(object sender, RoutedEventArgs e) {

            //blocking this button 
            UnblockButton.IsEnabled = false;

            //checking if the enable button is disabled; if so, enable it
            if (!BlockButton.IsEnabled) BlockButton.IsEnabled = true;

            //Using the unblock method on the manager
            manager.Unblock();

            //Changing the status to not active
            ActiveLabel.Content = "Not Active!";
            ActiveLabel.Foreground = Brushes.Red;
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e) {

            //Show the credit and help messagebox. Store the result of the visit yes/no into the boxResult
            MessageBoxResult boxResult = MessageBox.Show(
            "Usage:\n" +
            "Enable solo session before running GTA Online, or Enable solo session while in-game and go to 'Online -> Find a new session' to play alone.\n\n" +
            "Command Help:\n" +
            "Enable: enable the solo session.\n" +
            "Disable: let you join into populate session again\n\n" +
            "Credit:\n" +
            "Author: Asynchronousx\n" +
            "Site: https://github.com/Asynchronousx) \n" +
            "Visit the site?",
            "Help & Credit",
            MessageBoxButton.YesNo);

           //if the answer of the box result is yes, open the website
           if(boxResult == MessageBoxResult.Yes) {
                System.Diagnostics.Process.Start("https://github.com/Asynchronousx");
            }

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) {
            System.Environment.Exit(1);
        }


        //Method that allow the user to drag the borderless form while pressing the mouse's left button.
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

    }
}
